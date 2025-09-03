using Moq;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.UseCase.Properties.AddProperty;
using PropertyManager.Domain.Owners.Errors;
using PropertyManager.Domain.Owners.Repositories;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Errors;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Test.Application.UseCase.Properties.AddProperty
{
    /// <summary>
    /// Pruebas unitarias para la clase AddPropertyCommandHandler.
    /// Valida todos los escenarios posibles de creación de propiedad.
    /// </summary>
    [TestFixture]
    public class AddPropertyCommandHandlerTest
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private AddPropertyCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _handler = new AddPropertyCommandHandler(
                _propertyRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _ownerRepositoryMock.Object
            );
        }

        /// <summary>
        /// Verifica que se retorne error si el año de la propiedad es mayor al actual.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsFailure_WhenYearIsInFuture()
        {
            // Arrange
            var command = new AddPropertyCommand(
                "Propiedad Futuro",
                "Dirección Futuro",
                100000,
                string.Empty, // CodeInternal is required but can be initialized as empty
                DateTime.UtcNow.Year + 1,
                1
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(PropertyError.InvalidYear(command.Year).Code));
            });
        }

        /// <summary>
        /// Verifica que se retorne error si el precio de la propiedad es menor o igual a cero.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsFailure_WhenPriceIsZeroOrNegative()
        {
            // Arrange
            var command = new AddPropertyCommand(
                "Propiedad Sin Precio",
                "Dirección Sin Precio",
                0,
                string.Empty, // CodeInternal is required but can be initialized as empty
                DateTime.UtcNow.Year,
                1
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(PropertyError.InvalidPrice(command.Price).Code));
            });

            // Arrange negativo
            var commandNegativo = new AddPropertyCommand(
                "Propiedad Precio Negativo",
                "Dirección Negativa",
                -50000,
                string.Empty, // CodeInternal is required but can be initialized as empty
                DateTime.UtcNow.Year,
                1
            );

            // Act
            var resultNegativo = await _handler.Handle(commandNegativo, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(resultNegativo.IsFailure, Is.True);
                Assert.That(resultNegativo.Error.Code, Is.EqualTo(PropertyError.InvalidPrice(commandNegativo.Price).Code));
            });
        }

        /// <summary>
        /// Verifica que se retorne error si el propietario no existe.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsFailure_WhenOwnerDoesNotExist()
        {
            // Arrange
            var command = new AddPropertyCommand(
                "Propiedad Sin Dueño",
                "Dirección Sin Dueño",
                150000,
                string.Empty, // CodeInternal is required but can be initialized as empty
                DateTime.UtcNow.Year,
                99
            );

            _ownerRepositoryMock
                .Setup(r => r.ExistAsync(command.IdOwner, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.Code, Is.EqualTo(OwnerError.OwnerNotFound(command.IdOwner).Code));
            });
        }

        /// <summary>
        /// Verifica que se cree la propiedad correctamente cuando todos los datos son válidos.
        /// </summary>
        [Test]
        public async Task Handle_CreatesPropertySuccessfully_WhenDataIsValid()
        {
            // Arrange
            var command = new AddPropertyCommand(
                "Propiedad Correcta",
                "Dirección Correcta",
                200000,
                string.Empty, // CodeInternal is required but can be initialized as empty
                DateTime.UtcNow.Year,
                2
            );

            _ownerRepositoryMock
                .Setup(r => r.ExistAsync(command.IdOwner, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _propertyRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
                .Callback<Property, CancellationToken>((p, _) => p.IdProperty = 10)
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Value, Is.EqualTo(10));
            });
        }

        /// <summary>
        /// Verifica que se asignen correctamente los valores CodeInternal y CreatedAt al crear la propiedad.
        /// </summary>
        [Test]
        public async Task Handle_AssignsCodeInternalAndCreatedAt_WhenPropertyIsCreated()
        {
            // Arrange
            var command = new AddPropertyCommand(
                "Propiedad Código",
                "Dirección Código",
                300000,
                string.Empty, // CodeInternal is required but can be initialized as empty
                DateTime.UtcNow.Year,
                3
            );

            _ownerRepositoryMock
                .Setup(r => r.ExistAsync(command.IdOwner, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            Property? propertyAgregada = null;
            _propertyRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
                .Callback<Property, CancellationToken>((p, _) =>
                {
                    p.IdProperty = 20;
                    propertyAgregada = p;
                })
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Value, Is.EqualTo(20));
                Assert.That(propertyAgregada, Is.Not.Null);
                Assert.That(string.IsNullOrWhiteSpace(propertyAgregada.CodeInternal), Is.False);
            });

            Assert.That(propertyAgregada.CreatedAt, Is.Not.Null);
        }
    }
}