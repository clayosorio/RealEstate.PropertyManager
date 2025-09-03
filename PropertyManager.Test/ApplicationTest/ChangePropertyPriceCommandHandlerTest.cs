using Moq;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.UseCase.Properties.ChagePropertyPrice;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Errors;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Test.Application.UseCase.Properties.ChagePropertyPrice
{
    /// <summary>
    /// Pruebas unitarias para la clase ChangePropertyPriceCommandHandler.
    /// Valida todos los escenarios posibles de cambio de precio de propiedad.
    /// </summary>
    [TestFixture]
    public class ChangePropertyPriceCommandHandlerTest
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private ChangePropertyPriceCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new ChangePropertyPriceCommandHandler(
                _propertyRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        /// <summary>
        /// Verifica que se retorne error si la propiedad no existe.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsFailure_WhenPropertyDoesNotExist()
        {
            // Arrange
            var command = new ChangePropertyPriceCommand(99, 500000);

            _propertyRepositoryMock
                .Setup(r => r.GetByIdAsync(command.IdProperty, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Property?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(PropertyError.PropertyAlreadyExists(nameof(Property), command.IdProperty).Code));
            });
        }

        /// <summary>
        /// Verifica que se actualice el precio correctamente cuando la propiedad existe.
        /// </summary>
        [Test]
        public async Task Handle_UpdatesPriceSuccessfully_WhenPropertyExists()
        {
            // Arrange
            var property = new Property
            {
                IdProperty = 1,
                Name = "Propiedad Prueba",
                Address = "Dirección Prueba",
                Price = 100000,
                CodeInternal = "ABC123",
                Year = 2020,
                IdOwner = 1,
                Owner = new Owner
                {
                    IdOwner = 1,
                    Name = "Dueño Prueba",
                    Address = "Dirección Dueño",
                    Photo = "photo_placeholder",
                    UserName = "username_placeholder",
                    Email = "email_placeholder@test.com",
                    PasswordHash = "password_hash_placeholder",
                    PasswordSalt = "password_salt_placeholder"
                }
            };

            var command = new ChangePropertyPriceCommand(property.IdProperty, 250000);

            _propertyRepositoryMock
                .Setup(r => r.GetByIdAsync(command.IdProperty, It.IsAny<CancellationToken>()))
                .ReturnsAsync(property);

            _propertyRepositoryMock
                .Setup(r => r.Update(property));

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(property.Price, Is.EqualTo(command.NewPrice));
            });

            Assert.That(property.UpdatedAt, Is.Not.Null);
            Assert.That(property.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(5)));
        }

        /// <summary>
        /// Verifica que se llame a Update y SaveChangesAsync cuando la propiedad existe.
        /// </summary>
        [Test]
        public async Task Handle_CallsUpdateAndSaveChanges_WhenPropertyExists()
        {
            // Arrange
            var property = new Property
            {
                IdProperty = 2,
                Name = "Propiedad Test",
                Address = "Dirección Test",
                Price = 150000,
                CodeInternal = "XYZ789",
                Year = 2021,
                IdOwner = 2,
                Owner = new Owner()
                {
                    IdOwner = 2,
                    Name = "Dueño Test",
                    Address = "Dirección Test",
                    Photo = "photo_url_placeholder",
                    UserName = "username_placeholder",
                    Email = "email_placeholder@test.com",
                    PasswordHash = "password_hash_placeholder",
                    PasswordSalt = "password_salt_placeholder"
                }
            };

            var command = new ChangePropertyPriceCommand(property.IdProperty, 300000);

            _propertyRepositoryMock
                .Setup(r => r.GetByIdAsync(command.IdProperty, It.IsAny<CancellationToken>()))
                .ReturnsAsync(property);

            var updateCalled = false;
            _propertyRepositoryMock
                .Setup(r => r.Update(property))
                .Callback(() => updateCalled = true);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(updateCalled, Is.True);
            });
        }
    }
}