using Moq;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.UseCase.Properties.UpdateProperty;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Errors;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Test.Application.UseCase.Properties.UpdateProperty
{
    /// <summary>
    /// Pruebas unitarias para la clase UpdatePropertyCommandHandler.
    /// Valida todos los escenarios posibles de actualización de propiedad.
    /// </summary>
    [TestFixture]
    public class UpdatePropertyCommandHandlerTest
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private UpdatePropertyCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdatePropertyCommandHandler(
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
            var command = new UpdatePropertyCommand(99, "Nuevo Nombre", "Nueva Dirección", 0, "", 0);

            _propertyRepositoryMock
                .Setup(r => r.GetByIdAsync(command.IdProperty, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Property?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(PropertyError.PropertyNotFound(nameof(Property), command.IdProperty).Code));
            });
        }

        /// <summary>
        /// Verifica que se actualicen correctamente el nombre y la dirección de la propiedad existente.
        /// </summary>
        [Test]
        public async Task Handle_UpdatesNameAndAddressSuccessfully_WhenPropertyExists()
        {
            // Arrange
            var property = new Property
            {
                IdProperty = 1,
                Name = "Nombre Original",
                Address = "Dirección Original",
                Price = 100000,
                CodeInternal = Guid.NewGuid().ToString(),
                Year = 2020,
                IdOwner = 1,
                Owner = new Owner
                {
                    IdOwner = 1,
                    Name = "Dueño Prueba",
                    Address = "Dirección Prueba",
                    Photo = "photo_url_prueba",
                    UserName = "username_prueba",
                    Email = "email@prueba.com",
                    PasswordHash = "hashed_password_prueba",
                    PasswordSalt = "salt_value_prueba"
                }
            };

            var command = new UpdatePropertyCommand(property.IdProperty, "Nombre Actualizado", "Dirección Actualizada", property.Price, property.CodeInternal, property.Year);

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
                Assert.That(property.Name, Is.EqualTo(command.Name));
                Assert.That(property.Address, Is.EqualTo(command.Address));
                Assert.That(property.UpdatedAt, Is.Not.Null);
            });
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
                Name = "Nombre Test",
                Address = "Dirección Test",
                Price = 150000,
                CodeInternal = Guid.NewGuid().ToString(),
                Year = 2021,
                IdOwner = 2,
                Owner = new Owner
                {
                    IdOwner = 2,
                    Name = "Dueño Test",
                    Address = "Dirección Test",
                    Photo = "photo_url_test",
                    UserName = "username_test",
                    Email = "email@test.com",
                    PasswordHash = "hashed_password",
                    PasswordSalt = "salt_value"
                }
            };

            var command = new UpdatePropertyCommand(property.IdProperty, "Nombre Nuevo", "Dirección Nueva", property.Price, property.CodeInternal, property.Year);

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