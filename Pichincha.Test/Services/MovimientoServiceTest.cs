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
        private readonly Mock<IMapper> _mapper;

        public MovimientoServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _movimientoService = new Mock<IMovimientoService>();
            _movimientoRepository = new Mock<IMovimientoRepository>();
            _cuentaRepository = new Mock<ICuentaRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async void Task_Delete_Post_Return_Successful()
        {
            //Arrange
            var entity = _fixture.Create<MovimientoEntity>();

            _movimientoRepository
                .Setup(service => service.GetAsync(entity.Id))
                .ReturnsAsync(entity);

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper.Object);
            var result = await handler.RemoveMovimientoById(entity.Id);

            //Assert  
            Assert.IsType<StatusDto>(result);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var entity = _fixture.Create<MovimientoEntity>();

            Guid id = Guid.NewGuid();
            _movimientoRepository
                .Setup(service => service.GetAsync(id));

            // Act
            var handler = new MovimientoService(_movimientoRepository.Object, _cuentaRepository.Object, _mapper.Object);

            //Assert  
            await Assert.ThrowsAsync<NotFoundException>(() => handler.RemoveMovimientoById(id));
        }
    }
}
