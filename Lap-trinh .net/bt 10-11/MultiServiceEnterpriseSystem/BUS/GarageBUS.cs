using System;
using System.Collections.Generic;
using System.Data;
using MultiServiceEnterpriseSystem.DAL;
using MultiServiceEnterpriseSystem.DTO;

namespace MultiServiceEnterpriseSystem.BUS
{
    public class GarageBUS
    {
        private readonly GarageDAL _dal = new();

        public List<KhachHangDTO> GetAllCustomers() => _dal.GetCustomers();

        public bool AddCustomer(KhachHangDTO kh)
        {
            if (string.IsNullOrWhiteSpace(kh.TenKH))
                throw new ArgumentException("Tên khách hàng không được để trống!");
            return _dal.InsertCustomer(kh);
        }

        public bool UpdateCustomer(KhachHangDTO kh)
        {
            if (string.IsNullOrWhiteSpace(kh.TenKH))
                throw new ArgumentException("Tên khách hàng không được để trống!");
            return _dal.UpdateCustomer(kh);
        }

        public bool DeleteCustomer(int maKH) => _dal.DeleteCustomer(maKH);

        public List<XeDTO> GetAllVehicles() => _dal.GetVehicles();

        public bool AddVehicle(XeDTO xe)
        {
            if (string.IsNullOrWhiteSpace(xe.BienSo))
                throw new ArgumentException("Biển số xe không được để trống!");
            if (xe.MaKH <= 0)
                throw new ArgumentException("Vui lòng chọn chủ xe (Khách hàng)!");
            return _dal.InsertVehicle(xe);
        }

        public bool DeleteVehicle(string bienSo) => _dal.DeleteVehicle(bienSo);

        public List<PhuTungDTO> GetAllParts() => _dal.GetParts();

        public bool AddPart(PhuTungDTO pt)
        {
            if (string.IsNullOrWhiteSpace(pt.TenPhuTung))
                throw new ArgumentException("Tên phụ tùng không được để trống!");
            if (pt.DonGia < 0)
                throw new ArgumentException("Đơn giá phụ tùng không được âm!");
            if (pt.SoLuongTon < 0)
                throw new ArgumentException("Số lượng tồn kho không được âm!");

            return _dal.InsertPart(pt);
        }

        public bool UpdatePart(PhuTungDTO pt)
        {
            if (string.IsNullOrWhiteSpace(pt.TenPhuTung))
                throw new ArgumentException("Tên phụ tùng không được để trống!");
            if (pt.DonGia < 0)
                throw new ArgumentException("Đơn giá phụ tùng không được âm!");
            if (pt.SoLuongTon < 0)
                throw new ArgumentException("Số lượng tồn kho không được âm!");

            return _dal.UpdatePart(pt);
        }

        public bool DeletePart(int maPT) => _dal.DeletePart(maPT);

        public int CreateRepairOrder(string bienSo, string noiDung, List<ChiTietSuaChuaDTO> details)
        {
            if (string.IsNullOrWhiteSpace(bienSo))
                throw new ArgumentException("Vui lòng chọn biển số xe tiếp nhận sửa chữa!");
            if (details == null || details.Count == 0)
                throw new ArgumentException("Phiếu sửa chữa phải có ít nhất 1 phụ tùng hoặc hạng mục tiền công!");

            return _dal.CreateRepairOrder(bienSo, noiDung, details);
        }

        public decimal GetTotalRevenue() => _dal.GetTotalRevenue();
        public DataTable GetTopPartsReport() => _dal.GetTopPartsReport();
    }
}

