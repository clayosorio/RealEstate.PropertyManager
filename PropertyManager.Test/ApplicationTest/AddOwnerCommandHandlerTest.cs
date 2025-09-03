using Moq;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Application.UseCase.Owners.AddOwner;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Owners.Errors;
using PropertyManager.Domain.Owners.Repositories;

namespace PropertyManager.Test.Application.UseCase.Owners.AddOwner
{
    /// <summary>
    /// Pruebas unitarias para la clase AddOwnerCommandHandler.
    /// Valida todos los escenarios posibles de creación de propietario.
    /// </summary>
    [TestFixture]
    public class AddOwnerCommandHandlerTest
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPasswordService> _passwordServiceMock;
        private AddOwnerCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _handler = new AddOwnerCommandHandler(
                _ownerRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _passwordServiceMock.Object
            );
        }

        /// <summary>
        /// Verifica que se retorne error si el nombre de usuario ya existe.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsFailure_WhenUserNameAlreadyExists()
        {
            // Arrange
            var command = new AddOwnerCommand(
                "Nombre Prueba",
                "Dirección Prueba",
                "FotoPrueba.jpg",
                System.DateTime.UtcNow.AddYears(-30),
                "usuarioPrueba",
                "correo@prueba.com",
                "passwordPrueba"
            );

            _ownerRepositoryMock
                .Setup(r => r.ExistsByUserNameAsync(command.UserName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(OwnerError.OwnerAlreadyExists(nameof(Owner), command.UserName).Code, result.Error.Code);
        }

        /// <summary>
        /// Verifica que se cree el propietario correctamente cuando el usuario no existe.
        /// </summary>
        [Test]
        public async Task Handle_CreatesOwnerSuccessfully_WhenUserNameDoesNotExist()
        {
            // Arrange
            var command = new AddOwnerCommand(
                "Nombre Prueba",
                "Dirección Prueba",
                "FotoPrueba.jpg",
                System.DateTime.UtcNow.AddYears(-30),
                "usuarioNuevo",
                "correo@nuevo.com",
                "passwordNuevo"
            );

            _ownerRepositoryMock
                .Setup(r => r.ExistsByUserNameAsync(command.UserName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            string hash = "hashPrueba";
            string salt = "saltPrueba";
            _passwordServiceMock
                .Setup(p => p.CreatePasswordHash(command.Password, out hash, out salt));

            _ownerRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Owner>(), It.IsAny<CancellationToken>()))
                .Callback<Owner, CancellationToken>((o, _) => o.IdOwner = 1) // <-- Aquí el cambio
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.Greater(result.Value, 0);
        }
        /// <summary>
        /// Verifica que se retorne el IdOwner correcto después de guardar el propietario.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsCorrectOwnerId_AfterSave()
        {
            // Arrange
            var command = new AddOwnerCommand(
                "Nombre Prueba",
                "Dirección Prueba",
                "FotoPrueba.jpg",
                System.DateTime.UtcNow.AddYears(-30),
                "usuarioUnico",
                "correo@unico.com",
                "passwordUnico"
            );

            _ownerRepositoryMock
                .Setup(r => r.ExistsByUserNameAsync(command.UserName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            string hash = "hashUnico";
            string salt = "saltUnico";
            _passwordServiceMock
                .Setup(p => p.CreatePasswordHash(command.Password, out hash, out salt));

            Owner? ownerAgregado = null;
            _ownerRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Owner>(), It.IsAny<CancellationToken>()))
                .Callback<Owner, CancellationToken>((o, _) =>
                {
                    o.IdOwner = 123;
                    ownerAgregado = o;
                })
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(123, result.Value);
            Assert.NotNull(ownerAgregado);
            Assert.AreEqual(hash, ownerAgregado.PasswordHash);
            Assert.AreEqual(salt, ownerAgregado.PasswordSalt);
        }
    }
}