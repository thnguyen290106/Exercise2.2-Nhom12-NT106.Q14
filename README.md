# Ứng Dụng Quản Lý Người Dùng

Bài tập Exercise2.2 - Môn Lập Trình Mạng Căn Bản - Trường ĐH CNTT - ĐHQG TP.HCM (UIT)

## 📖 Giới Thiệu

Ứng dụng thực hiện quản lý người dùng với chức năng chính là đăng kí tài khoản, đăng nhập, và hiển thị thông tin cơ bản của người dùng. Dữ liệu được lưu bằng SQLite, mật khẩu được mã hóa SHA-256, giao diện xây dựng trên C# Windows Forms.

### Mục Đích Bài Tập

- Xây dựng ứng dụng desktop với giao diện thân thiện.
- Thực hiện chức năng xác thực người dùng hiệu quả (đăng kí, đăng nhập).
- Mã hóa mật khẩu khi lưu trữ.
- Lưu thông tin, kiểm tra trùng lặp username/email và xử lý logic đầu vào.

## 👥 Thành Viên Nhóm

**Nhóm 12 - Lớp NT106.Q14**

| MSSV     | Họ và Tên         |
|----------|-------------------|
| 24521557 | Phạm Đức Tài      |
| 24520331 | Trần Minh Đức     |
| 24521213 | Trần Thanh Nguyên |
| 24521538 | Trần Sơn          |
| 24521560 | Trần Văn Tài      |

## 🛠️ Công Nghệ Sử Dụng

- Ngôn ngữ lập trình: C#
- .NET Framework 4.7.2
- Windows Forms
- Visual Studio 2022
- SQLite (thay thế SQL Server cho đơn giản)

## ✨ Tính Năng

### 1. đăng kí tài khoản

- Nhập username, email, mật khẩu, xác nhận mật khẩu.
- Kiểm tra hợp lệ đầu vào, kiểm tra trùng lặp username và email.
- Mã hóa mật khẩu trước khi lưu bằng SHA-256.

### 2. Đăng nhập

- Nhập username và mật khẩu.
- Kiểm tra thông tin đăng nhập (so sánh mật khẩu đã mã hóa).
- Thông báo lỗi hợp lý khi đăng nhập thất bại hoặc điền thiếu thông tin.

### 3. Hiển thị thông tin người dùng

- Sau khi đăng nhập, hiển thị thông tin cơ bản: username và email của người dùng.

## 💻 Yêu Cầu Hệ Thống

- Windows 7/8/10/11
- .NET Framework 4.7.2 trở lên
- Visual Studio (để build từ source code)

## 📥 Hướng Dẫn Cài Đặt

### Bước 1: Clone Repository

```
git clone https://github.com/thnguyen290106/Exercise2.2-Nhom12-NT106.Q14.git
cd Exercise2.2-Nhom12-NT106.Q14
```


### Bước 2: Mở Project

1. Mở Visual Studio
2. File → Open → Project/Solution
3. Chọn file `.sln` trong thư mục đã clone

### Bước 3: Build & Chạy Ứng Dụng

- Build bằng `Ctrl + Shift + B`
- Chạy bằng `F5` (Debug) hoặc `Ctrl + F5` (không debug)

## 📚 Hướng Dẫn Sử Dụng

### đăng kí tài khoản mới

1. Mở ứng dụng, chọn đăng kí
2. Điền username, email, mật khẩu, xác nhận mật khẩu
3. Nhấn "Đăng kí"

### Đăng nhập hệ thống

1. Nhập username và mật khẩu
2. Nhấn "Đăng nhập" để vào giao diện chính

### Xem thông tin tài khoản

- Sau đăng nhập, màn hình chính sẽ hiển thị username và email của tài khoản đăng nhập.

## 🖼️ Giao Diện Ứng Dụng

### Màn Hình Đăng Nhập

![Giao diện đăng nhập](https://sv2.anhsieuviet.com/2025/09/29/imageda4cf264fb30f922.png)

*Màn hình đăng nhập với các trường username và password*

### Màn Hình đăng kí

![Giao diện đăng kí](https://sv2.anhsieuviet.com/2025/09/29/image48a878a566f0b40d.png)

*Màn hình đăng kí tài khoản mới với form nhập thông tin đầy đủ*

### Màn Hình Chính

![Giao diện chính](https://sv2.anhsieuviet.com/2025/09/29/image20b064c5674636e2.png)

*Giao diện chính sau khi đăng nhập thành công, hiển thị danh sách người dùng*

## 📝 Ghi Chú

- Dữ liệu lưu tại file SQLite `TaiKhoan.db`.
- Mật khẩu được lưu dưới dạng SHA-256 hash.
- Hệ thống kiểm tra đầu vào và thông báo lỗi rõ ràng.
