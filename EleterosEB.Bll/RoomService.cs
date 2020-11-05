using EleterosEB.Data;
using EleterosEB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EleterosEB.Bll
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CreateRoom(Room newRoom)
        {
            _unitOfWork.RoomRepository.Add(newRoom);
            return _unitOfWork.CommitAsync();

        }

        public Task<bool> DeleteRoom(Room room)
        {
            _unitOfWork.RoomRepository.Delete(room);
            return _unitOfWork.CommitAsync();
        }

        public Task<bool> UpdateRoom(Room room)
        {
            _unitOfWork.RoomRepository.Update(room);
            return _unitOfWork.CommitAsync();
        }

        public Task<IReadOnlyList<Room>> GetAllRoomsAsync()
        {
            return _unitOfWork.RoomRepository.ListAsync();
        }

        public async Task<Room> GetRoomByNameAsync(string name)
        {
            var query =  await _unitOfWork.RoomRepository.ListAsync();

            return query.FirstOrDefault(r => r.Name == name);
        }

        public Task<Room> GetRoomByIdAsync(int id)
        {
            return _unitOfWork.RoomRepository.GetByIdAsync(id);
        }

    }
}
