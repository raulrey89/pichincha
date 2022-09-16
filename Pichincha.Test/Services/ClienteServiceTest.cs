using AutoFixture;
using Moq;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Models.DTOs;
using Pichincha.Models.Request;
using Pichincha.Services.Exceptions;
using Pichincha.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Test.Services
{
    public class ClienteServiceTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IClienteRepository> _clienteRepository;

        public ClienteServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _clienteRepository = new Mock<IClienteRepository>();
        }

        [Fact]
        public async Task Task_Cliente_GetAll_RetornaValor()
        {

            //Arrange
            var entityList = _fixture.CreateMany<ClienteEntity>().ToList();

            _clienteRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entityList);

            // Act
            var handler = new ClienteService(_clienteRepository.Object);
            var result = await handler.GetClientes();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Task_Cliente_GetById_RetornaValor()
        {

            //Arrange
            var entity = _fixture.Create<ClienteEntity>();

            _clienteRepository.Setup(repo => repo.GetAsync(entity.Id)).ReturnsAsync(entity);

            // Act
            var handler = new ClienteService(_clienteRepository.Object);
            var result = await handler.GetClienteById(entity.Id);

            // Assert
            var viewResult = Assert.IsType<PersonaClienteReadDto>(result);
            var prueba = (PersonaClienteReadDto)result;

            Assert.Equal(prueba.Id, entity.Id);
        }

        [Fact]
        public async Task Task_Cliente_GetById_RetornaBadRequest()
        {
            //Arrange
            var entity = _fixture.Create<ClienteEntity>();

            _clienteRepository.Setup(repo => repo.GetAsync(entity.Id));

            // Act
            var handler = new ClienteService(_clienteRepository.Object);

            // Act
            await Assert.ThrowsAsync<BadRequestException>(() => handler.GetClienteById(entity.Id));
        }
        [Fact]
        public async Task Task_Movimiento_Post_Success()
        {
            //Arrange
            var entity = _fixture.Create<PersonaCliente>();

            // Act
            var handler = new ClienteService(_clienteRepository.Object);
            var retorno = await handler.AddCliente(entity);

            // Assert
            Assert.True(retorno.IsSuccess);
        }
        [Fact]
        public async Task Task_Movimiento_Put_Success()
        {
            //Arrange
            var entity = _fixture.Create<PersonaCliente>();
            var entityCliente = _fixture.Create<ClienteEntity>();

            _clienteRepository
                .Setup(service => service.GetAsync(entityCliente.Id)).ReturnsAsync(entityCliente);

            // Act
            var handler = new ClienteService(_clienteRepository.Object);
            var retorno = await handler.UpdateCliente(entityCliente.Id, entity);

            // Assert
            Assert.True(retorno.IsSuccess);
        }

        [Fact]
        public async Task Task_Movimiento_Put_BadRequest()
        {
            //Arrange
            var entity = _fixture.Create<PersonaCliente>();
            var entityCliente = _fixture.Create<ClienteEntity>();

            _clienteRepository
                .Setup(service => service.GetAsync(entityCliente.Id));

            // Act
            var handler = new ClienteService(_clienteRepository.Object);

            //Assert  
            await Assert.ThrowsAsync<BadRequestException>(() => handler.UpdateCliente(entityCliente.Id, entity));
        }
        [Fact]
        public async void Task_Cliente_Delete_Return_Successful()
        {
            //Arrange
            var entity = _fixture.Create<ClienteEntity>();

            _clienteRepository
                .Setup(service => service.GetAsync(entity.Id))
                .ReturnsAsync(entity);

            // Act
            var handler = new ClienteService(_clienteRepository.Object);
            var result = await handler.RemoveClienteById(entity.Id);

            //Assert  
            Assert.IsType<StatusDto>(result);
        }

        [Fact]
        public async void Task_Cliente_Delete_Return_NotFoundResult()
        {
            //Arrange
            var entity = _fixture.Create<ClienteEntity>();

            Guid id = Guid.NewGuid();
            _clienteRepository
                .Setup(service => service.GetAsync(id));

            // Act
            var handler = new ClienteService(_clienteRepository.Object);

            //Assert  
            await Assert.ThrowsAsync<NotFoundException>(() => handler.RemoveClienteById(id));
        }
    }
}
