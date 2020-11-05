using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data;
using EleterosEB.Domain;

namespace EleterosEB.Bll
{
    public class ClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateClient(Client newClient)
        {
            _unitOfWork.ClientRepository.Add(newClient);
            return _unitOfWork.CommitAsync();

        }

        public Task<bool> DeleteClient(Client client)
        {
            _unitOfWork.ClientRepository.Delete(client);
            return _unitOfWork.CommitAsync();
        }

        public Task<bool> UpdateClient(Client client)
        {
            _unitOfWork.ClientRepository.Update(client);
            return _unitOfWork.CommitAsync();
        }

        public Task<IReadOnlyList<Client>> GetAllClientsAsync()
        {
            return _unitOfWork.ClientRepository.ListAsync(c => c.Patients);
        }

        public async Task<Client> GetClientByNameAsync(string fullName)
        {
            var query = await _unitOfWork.ClientRepository.ListAsync();

            return query.FirstOrDefault(c => c.FullName == fullName);
        }

        public Task<Client> GetClientsByIdAsync(int id)
        {
            return _unitOfWork.ClientRepository.GetByIdAsync(id);
        }

    }
}
