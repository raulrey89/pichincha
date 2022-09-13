using AutoFixture;
using Moq;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Models.DTOs;
using Pichincha.Services.Exceptions;
using Pichincha.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Test.Services
{
    public class CuentaServiceTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICuentaRepository> _cuentaRepository;

        public CuentaServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _cuentaRepository = new Mock<ICuentaRepository>();
        }

        [Fact]
        public async Task Task_Cliente_GetAll_RetornaValor()
        {

            //Arrange
            var entityList = _fixture.CreateMany<CuentaEntity>().ToList();

            _cuentaRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entityList);

            // Act
            var handler = new CuentaService(_cuentaRepository.Object);
            var result = await handler.GetCuentas();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Task_Cliente_GetById_RetornaValor()
        {

            //Arrange
            var entity = _fixture.Create<CuentaEntity>();

            _cuentaRepository.Setup(repo => repo.GetAsync(entity.Id)).ReturnsAsync(entity);

            // Act
            var handler = new CuentaService(_cuentaRepository.Object);
            var result = await handler.GetCuentaById(entity.Id);

            // Assert
            var viewResult = Assert.IsType<CuentaReadDto>(result);
            var prueba = (CuentaReadDto)result;

            Assert.Equal(prueba.Id, entity.Id);
        }

        [Fact]
        public async Task Task_Cliente_GetById_RetornaBadRequest()
        {
            //Arrange
            var entity = _fixture.Create<CuentaEntity>();

            _cuentaRepository.Setup(repo => repo.GetAsync(entity.Id));

            // Act
            var handler = new CuentaService(_cuentaRepository.Object);

            // Act
            await Assert.ThrowsAsync<BadRequestException>(() => handler.GetCuentaById(entity.Id));
        }

        [Fact]
        public async void Task_Delete_Post_Return_Successful()
        {
            //Arrange
            var entity = _fixture.Create<CuentaEntity>();

            _cuentaRepository
                .Setup(service => service.GetAsync(entity.Id))
                .ReturnsAsync(entity);

            // Act
            var handler = new CuentaService(_cuentaRepository.Object);
            var result = await handler.RemoveCuentaById(entity.Id);

            //Assert  
            Assert.IsType<StatusDto>(result);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var entity = _fixture.Create<CuentaEntity>();

            Guid id = Guid.NewGuid();
            _cuentaRepository
                .Setup(service => service.GetAsync(id));

            // Act
            var handler = new CuentaService(_cuentaRepository.Object);

            //Assert  
            await Assert.ThrowsAsync<NotFoundException>(() => handler.RemoveCuentaById(id));
        }
    }
}
