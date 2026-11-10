using System;
using System.Collections.Generic;
using System.Data;
using MultiServiceEnterpriseSystem.DAL;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.BUS
{
    public class KaraokeBUS
    {
        private readonly KaraokeDAL _dal = new();

        public List<PhongHatDTO> GetAllRooms() => _dal.GetRooms();

        public bool AddRoom(PhongHatDTO room)
        {
            if (string.IsNullOrWhiteSpace(room.TenPhong))
                throw new ArgumentException("Tên phòng hát không được để trống!");
            if (room.GiaTheoGio < 0)
                throw new ArgumentException("Giá phòng theo giờ không được là số âm!");

            return _dal.InsertRoom(room);
        }

        public bool UpdateRoom(PhongHatDTO room)
        {
            if (string.IsNullOrWhiteSpace(room.TenPhong))
                throw new ArgumentException("Tên phòng hát không được để trống!");
            if (room.GiaTheoGio < 0)
                throw new ArgumentException("Giá phòng theo giờ không được là số âm!");

            return _dal.UpdateRoom(room);
        }

        public bool DeleteRoom(int roomId) => _dal.DeleteRoom(roomId);

        public List<DichVuDTO> GetAllServices() => _dal.GetServices();

        public bool AddService(DichVuDTO dv)
        {
            if (string.IsNullOrWhiteSpace(dv.TenDV))
                throw new ArgumentException("Tên dịch vụ không được để trống!");
            if (dv.DonGia < 0)
                throw new ArgumentException("Đơn giá dịch vụ không được âm!");
            if (dv.TonKho < 0)
                throw new ArgumentException("Số lượng tồn kho không được âm!");

            return _dal.InsertService(dv);
        }

        public bool UpdateService(DichVuDTO dv)
        {
            if (string.IsNullOrWhiteSpace(dv.TenDV))
                throw new ArgumentException("Tên dịch vụ không được để trống!");
            if (dv.DonGia < 0)
                throw new ArgumentException("Đơn giá dịch vụ không được âm!");
            if (dv.TonKho < 0)
                throw new ArgumentException("Số lượng tồn kho không được âm!");

            return _dal.UpdateService(dv);
        }

        public bool DeleteService(int serviceId) => _dal.DeleteService(serviceId);

        public List<ChiTietDichVuDTO> GetInvoiceServices(int invoiceId) => _dal.GetInvoiceServices(invoiceId);

        public int OpenRoom(int roomId) => _dal.OpenRoom(roomId);

        public void OrderService(int invoiceId, int serviceId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng dịch vụ gọi phải lớn hơn 0!");
            if (unitPrice < 0)
                throw new ArgumentException("Đơn giá dịch vụ không hợp lệ!");

            _dal.OrderService(invoiceId, serviceId, quantity, unitPrice);
        }

        public (decimal roomAmount, decimal serviceAmount, decimal totalAmount, double hours) CheckoutRoom(int roomId, int invoiceId)
        {
            return _dal.CheckoutRoom(roomId, invoiceId);
        }

        public decimal GetTotalRevenue() => _dal.GetTotalRevenue();
        public int GetOccupiedRoomCount() => _dal.GetOccupiedRoomCount();
        public DataTable GetTopServicesReport() => _dal.GetTopServicesReport();
    }
}

