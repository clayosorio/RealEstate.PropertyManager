using Moq;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Application.UseCase.Authentications;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Owners.Errors;
using PropertyManager.Domain.Owners.Repositories;

namespace PropertyManager.Test.Application.UseCase.Authentications
{
    /// <summary>
    /// Pruebas unitarias para la clase SingInQueryHandler.
    /// Valida todos los escenarios posibles de autenticación.
    /// </summary>
    [TestFixture]
    public class SingInQueryHandlerTest
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private Mock<IPasswordService> _passwordServiceMock;
        private Mock<ITokenProvider> _tokenProviderMock;
        private SingInQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _passwordServiceMock = new Mock<IPasswordService>();
            _tokenProviderMock = new Mock<ITokenProvider>();
            _handler = new SingInQueryHandler(
                _ownerRepositoryMock.Object,
                _passwordServiceMock.Object,
                _tokenProviderMock.Object
            );
        }

        /// <summary>
        /// Verifica que se retorne error de credenciales inválidas si el usuario no existe.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsFailure_WhenOwnerDoesNotExist()
        {
            // Arrange
            var query = new SingInQuery("usuarioPrueba", "passwordPrueba");
            _ownerRepositoryMock
                .Setup(r => r.GetByUserNameAsync(query.userName, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Owner?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(AuthenticationError.InvalidCredentials().Code, result.Error.Code);
        }

        /// <summary>
        /// Verifica que se retorne error de credenciales inválidas si la contraseña es incorrecta.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsFailure_WhenPasswordIsInvalid()
        {
            // Arrange
            var owner = new Owner
            {
                IdOwner = 1,
                Name = "Nombre Prueba",
                Address = "Dirección Prueba",
                Photo = "FotoPrueba.jpg",
                Birthday = System.DateTime.UtcNow.AddYears(-30),
                UserName = "usuarioPrueba",
                Email = "correo@prueba.com",
                PasswordHash = "hashPrueba",
                PasswordSalt = "saltPrueba",
                Properties = null
            };
            var query = new SingInQuery(owner.UserName, "passwordIncorrecta");

            _ownerRepositoryMock
                .Setup(r => r.GetByUserNameAsync(query.userName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(owner);

            _passwordServiceMock
                .Setup(p => p.VerifyPasswordHash(query.password, owner.PasswordHash, owner.PasswordSalt))
                .Returns(false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(AuthenticationError.InvalidCredentials().Code, result.Error.Code);
        }

        /// <summary>
        /// Verifica que se retorne el token correctamente cuando las credenciales son válidas.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsToken_WhenCredentialsAreValid()
        {
            // Arrange
            var owner = new Owner
            {
                IdOwner = 2,
                Name = "Nombre Correcto",
                Address = "Dirección Correcta",
                Photo = "FotoCorrecta.jpg",
                Birthday = System.DateTime.UtcNow.AddYears(-25),
                UserName = "usuarioCorrecto",
                Email = "correo@correcto.com",
                PasswordHash = "hashCorrecto",
                PasswordSalt = "saltCorrecto",
                Properties = null
            };
            var query = new SingInQuery(owner.UserName, "passwordCorrecta");
            var expectedToken = "tokenDePrueba";

            _ownerRepositoryMock
                .Setup(r => r.GetByUserNameAsync(query.userName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(owner);

            _passwordServiceMock
                .Setup(p => p.VerifyPasswordHash(query.password, owner.PasswordHash, owner.PasswordSalt))
                .Returns(true);

            _tokenProviderMock
                .Setup(t => t.Create(owner))
                .Returns(expectedToken);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(expectedToken, result.Value);
        }
    }
}