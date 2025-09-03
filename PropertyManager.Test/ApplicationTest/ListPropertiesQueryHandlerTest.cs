using Moq;
using PropertyManager.Application.UseCase.Properties.ListProperties;
using PropertyManager.Domain.Properties.Queries.Output;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Test.Application.UseCase.Properties.ListProperties
{
    /// <summary>
    /// Pruebas unitarias para ListPropertiesQueryHandler.
    /// </summary>
    [TestFixture]
    public class ListPropertiesQueryHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private ListPropertiesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new ListPropertiesQueryHandler(_propertyRepositoryMock.Object);
        }

        /// <summary>
        /// Verifica que se retorne correctamente una lista paginada de propiedades cuando existen resultados.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsPagedResult_WhenPropertiesExist()
        {
            // Arrange
            var properties = new List<PropertyOutputDto>
            {
                new PropertyOutputDto
                {
                    IdProperty = 1,
                    Name = "Casa 1",
                    Address = "Calle 123",
                    Price = 100000,
                    CodeInternal = "A001",
                    Year = 2020,
                    IdOwner = 10,
                    OwnerName = "Juan Pérez",
                    CreatedAt = System.DateTime.UtcNow
                }
            };
            var query = new ListPropertiesQuery("Casa", 50000, 150000, 1, 10);

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesAsync(query.Name, query.MinPrice, query.MaxPrice, query.Page, query.PageSize, It.IsAny<CancellationToken>()))
                .ReturnsAsync(properties);

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesCountAsync(query.Name, query.MinPrice, query.MaxPrice, It.IsAny<CancellationToken>()))
                .ReturnsAsync(properties.Count);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Value, Is.Not.Null);
            });

            Assert.Multiple(() =>
            {
                Assert.That(result.Value.TotalCount, Is.EqualTo(properties.Count));
                Assert.That(result.Value.Items, Is.EqualTo(properties));
                Assert.That(result.Value.Page, Is.EqualTo(query.Page));
                Assert.That(result.Value.PageSize, Is.EqualTo(query.PageSize));
            });
        }

        /// <summary>
        /// Verifica que se retorne una lista vacía y el total en cero cuando no existen propiedades.
        /// </summary>
        [Test]
        public async Task Handle_ReturnsEmptyPagedResult_WhenNoPropertiesExist()
        {
            // Arrange
            var query = new ListPropertiesQuery("NoExiste", null, null, 1, 10);

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesAsync(query.Name, query.MinPrice, query.MaxPrice, query.Page, query.PageSize, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PropertyOutputDto>());

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesCountAsync(query.Name, query.MinPrice, query.MaxPrice, It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Value, Is.Not.Null);
            });
            Assert.That(result.Value.TotalCount, Is.EqualTo(0));
            Assert.That(result.Value.Items, Is.Not.Null);
        }

        /// <summary>
        /// Verifica que los parámetros de paginación se asignen correctamente en el resultado.
        /// </summary>
        [Test]
        public async Task Handle_AssignsPaginationParametersCorrectly()
        {
            // Arrange
            var properties = new List<PropertyOutputDto>
            {
                new PropertyOutputDto
                {
                    IdProperty = 2,
                    Name = "Casa 2",
                    Address = "Calle 456",
                    Price = 200000,
                    CodeInternal = "A002",
                    Year = 2021,
                    IdOwner = 11,
                    OwnerName = "Ana Gómez",
                    CreatedAt = System.DateTime.UtcNow
                }
            };
            var query = new ListPropertiesQuery(null, null, null, 2, 5);

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesAsync(query.Name, query.MinPrice, query.MaxPrice, query.Page, query.PageSize, It.IsAny<CancellationToken>()))
                .ReturnsAsync(properties);

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesCountAsync(query.Name, query.MinPrice, query.MaxPrice, It.IsAny<CancellationToken>()))
                .ReturnsAsync(properties.Count);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Value.Page, Is.EqualTo(query.Page));
                Assert.That(result.Value.PageSize, Is.EqualTo(query.PageSize));
            });
        }
    }
}