# Eva de Eva - Clothing shop
## Tính năng

Cửa hàng thời trang thiết kế sẵn Eva de Eva muốn phát triển một trang web để quảng bá sản phẩm và thu hút khách hàng cũng như tham gia thị trường online. Các mẫu thiết kế của Eva de Eva hướng tới khách hàng nữ bao gồm các hạng mục quần áo. Các chức năng chính của ứng dụng như sau:
- Quản lý sản phẩm theo danh mục, màu sắc, kích thước
- Lựa chọn giỏ hàng, thanh toán tiện lợi
- Chương trình khách hàng thân thiết tích điểm, chia theo cấp bậc
- Mã khuyến mãi cho phép tạo thêm các voucher để gửi đến khách hàng
- Xuất báo cáo bằng file Excel .xlsx

## Công cụ sử dụng

Project sử dụng một số công cụ sau cho việc lập trình và quản lý dự án

- [ASP.NET Core MVC] - Công cụ chính để xây dựng phần mềm ở cả Back-end và Front-end
- [Microsoft SQL Server] - Sử dụng cho toàn bộ cơ sở dữ liệu
- [Microsoft Azure] - Hỗ trợ hosting cho Cơ sở dữ liệu, Webapp, build code từ Github
- [Boostrap] - Framework hỗ trợ về mặt giao diện người dùng
- [Identity Framework] - Framework hỗ trợ phân quyền
- [Entity Framework] - Framework hỗ trợ ánh xạ, truy xuất cơ sở dữ liệu
- [EPPlus] - Công cụ hỗ trợ xử lý file Excel
- [Toast Notification] - Công cụ hỗ trợ xử lý thông báo đẩy
- [Github] - Công cụ hỗ trợ quản lý mã nguồn
- [Asana] - Công cụ hỗ trợ quản lý Product Backlog của team Agile Scrum


## Cài đặt

Project này yêu cầu có một số công cụ sau
- [Visual Studio 2019] - Có lựa chọn ASP .NET and web development
- [Microsoft SQL Server 2019]
- [.NET Core 3.1 SDK]

Cập nhật _appsettings.json_
```sh
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ClothingShopConnectionString": "[Connection String tới DB]"
  }
```

Trong _Visual Studio_, mở _Tools/ NuGet Package Manager/ Package Manager Console_, default project chọn _ClothingShop.Entity_

```sh
Add-Migration InitialMigration
Update-database
```

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

[ASP.NET Core MVC]: <https://dotnet.microsoft.com/en-us/download/dotnet/3.1>
[Microsoft SQL Server]: <https://www.microsoft.com/en-US/sql-server/sql-server-downloads>
[Microsoft Azure]: <https://azure.microsoft.com/en-us/>
[Boostrap]: <https://getbootstrap.com/>
[Identity Framework]: <https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio>
[Entity Framework]: <https://entityframework.net/>
[EPPlus]: <https://www.epplussoftware.com/>
[Toast Notification]: <https://github.com/aspnetcorehero/ToastNotification>
[Github]: <https://github.com/>
[Asana]: <https://asana.com/>
[Visual Studio 2019]: <https://visualstudio.microsoft.com/vs/older-downloads/>
[Microsoft SQL Server 2019]: <https://www.microsoft.com/en-US/sql-server/sql-server-downloads>
[.NET Core 3.1 SDK]: <https://dotnet.microsoft.com/en-us/download/dotnet/3.1>
