using AutoFixture;
using AutoMapper;
using Moq;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Models.DTOs;
using Pichincha.Services.Exceptions;
using Pichincha.Services.Implementations;
using Pichincha.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Test.Services
{
    public class MovimientoServiceTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IMovimientoService> _movimientoService;
        private readonly Mock<IMovimientoRepository> _movimientoRepository;
        private readonly Mock<ICuentaRepository> _cuentaRepository;
        private readonly IMapper _mapper;

        public MovimientoServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _movimientoService = new Mock<IMovimientoService>();
            _movimientoRepository = new Mock<IMovimientoRepository>();
            _cuentaRepository = new Mock<ICuentaRepository>();
            _mapper = MapperHelper.InitMappings();
        }

        [Fact]
        public async Task Task_Cliente_GetAll_RetornaValor()
        {

            //Arrange
            var entityList = _fixture.CreateMany<MovimientoEntity>().ToList();

            _movimientoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entityList);

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper);
            var result = await handler.GetMovimientos();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Task_Movimiento_GetById_RetornaValor()
        {

            //Arrange
            var entity = _fixture.Create<MovimientoEntity>();

            _movimientoRepository.Setup(repo => repo.GetAsync(entity.Id)).ReturnsAsync(entity);

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper);
            var result = await handler.GetMovimientoById(entity.Id);

            // Assert
            var viewResult = Assert.IsType<MovimientoReadDto>(result);
            var prueba = (MovimientoReadDto)result;

            Assert.Equal(prueba.Id, entity.Id);
        }

        [Fact]
        public async Task Task_Movimiento_GetById_RetornaBadRequest()
        {
            //Arrange
            var entity = _fixture.Create<MovimientoEntity>();

            _movimientoRepository.Setup(repo => repo.GetAsync(entity.Id));

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper);

            // Act
            await Assert.ThrowsAsync<BadRequestException>(() => handler.GetMovimientoById(entity.Id));
        }

        [Fact]
        public async Task Task_Movimiento_Post_Success()
        {
            //Arrange
            var entity = _fixture.Create<MovimientoCreateDto>();
            var entityCuenta = _fixture.Create<CuentaEntity>();

            _cuentaRepository
                .Setup(service => service.GetAsync(entity.IdCuenta)).ReturnsAsync(entityCuenta);

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper);
            var retorno = await handler.AddMovimiento(entity);

            // Assert
            Assert.True(retorno.IsSuccess);
        }

        [Fact]
        public async Task Task_Movimiento_Post_BadRequest()
        {
            //Arrange
            var entity = _fixture.Create<MovimientoCreateDto>();
            var entityCuenta = _fixture.Create<CuentaEntity>();

            _cuentaRepository
                .Setup(service => service.GetAsync(entity.IdCuenta));

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper);

            //Assert  
            await Assert.ThrowsAsync<BadRequestException>(() => handler.AddMovimiento(entity));
        }

        [Fact]
        public async void Task_Movimiento_Delete_Return_Successful()
        {
            //Arrange
            var entity = _fixture.Create<MovimientoEntity>();

            _movimientoRepository
                .Setup(service => service.GetAsync(entity.Id))
                .ReturnsAsync(entity);

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper);
            var result = await handler.RemoveMovimientoById(entity.Id);

            //Assert  
            Assert.IsType<StatusDto>(result);
        }

        [Fact]
        public async void Task_Movimiento_Delete_Return_NotFoundResult()
        {
            //Arrange
            var entity = _fixture.Create<MovimientoEntity>();

            Guid id = Guid.NewGuid();
            _movimientoRepository
                .Setup(service => service.GetAsync(id));

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper);

            //Assert  
            await Assert.ThrowsAsync<NotFoundException>(() => handler.RemoveMovimientoById(id));
        }
    }
}
