
# BOOMAN SYSTEM

Đây là một hệ thống được xây dựng nhằm hỗ trợ các cơ sở kinh doanh lưu trú quản lý quy trình đặt phòng.



## Tech

* .NET Framework
* WPF
* MySQL


## Features

- Đăng nhập
- Đặt mật khẩu
- Quản lý tài khoản nhân viên
- Quản lý thông tin chung
- Quản lý phòng
- Xem sơ đồ phòng
- Quản lý dịch vụ
- Xem bảng dịch vụ
- Quản lý đơn đặt phòng
- Báo cáo thống kê


## Usage
* Bước 1: Tải MySQL, chạy file booman.sql
* Bước 2: Mở dự án, mở file MySQLDatabaseService.cs trong thư mục Services và sửa các thông tin sau:
    ```net
        # Tên server MySQL của bạn
        private string server = "localhost";
        # UID trong MySQL (thường để mặc định là 'root')
        private string uid = "root";
        # Mật khẩu của UID trong MySQL
        private string password = "abcdef";
    ```
* Bước 3: Chạy chương trình, có thể sử dụng tài khoản taikhoantest@gmail.com và mật khẩu là 123456 để đăng nhập
## Authors

- [@vnKhanhsDev](https://github.com/vnKhanhsDev) - Vũ Nam Khánh - 21012879
- Đinh Đức Khánh - 21012504
- Nguyễn Danh Điệp - 21012491
- Nguyễn Minh Tuấn - 21012903
- Phạm Minh Hiếu - 21012495


