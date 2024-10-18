# Streaming_App_Network_Project
"""
Trước khi sử dụng chương trình về đề tài "Xây dựng ứng dụng streaming" thì cần có các yêu cầu sau:
- Visual Studio 2022: đều có thể sử dụng được ở phiên bản Community hay Professional
- .NET SDK: đảm bảo đã cài đặt .NET 8 SDK để biên dịch và chạy dự án.
- Hệ điều hành Windows
Sau đây là các thư viện cần thiết cho chương trình (nếu chưa được cài đặt, bạn có thể cài đặt qua NuGet trên Visual Studio
- NAudio: thư viện dùng để xử lý âm thanh, ghi lại âm thanh từ server và phát lại audio trên client
- AForge.Video và AForge.Video.DirectShow: các thư viện này dùng để xử lý video và kết nối với thiết bị camera
Về môi trường: Thiết bị cần có microphone và camera để ứng dụng đầy đủ các chức năng của chương trình

Chương trình sau đây sử dụng các kiến thức như: lập trình C# và .NET, lập trình mạng, multithreading.

***Các chức năng của ứng dụng
Khi bạn bắt đầu chạy ứng dụng, form chọn "Who are you?" sẽ hiển thị và nơi đây sẽ giúp bạn chọn vai trò của mình trong ứng dụng, Server sẽ là người chạy chương trình đầu tiên và là người truyền dữ liệu stream cho client, đối với client sẽ là người thực hiện kết nối với server và là người nhận dữ liệu từ server

**Chức năng đối với Server
1. Bắt đầu stream (truyền dữ liệu cho Client)
- Sau khi bạn đã chọn bạn chính là server, form server sẽ hiển thị (hiện tại chương trình rất đơn giản ở phía server) chỉ có một hoạt động là stream (truyền dữ liệu cho client)
Sau khi đã thành công vào form, hãy nhấn nút "Bắt đầu stream", camera và microphone sẽ bắt đầu nhận và bắt đầu truyền dữ liệu đến với client.
**Chức năng đối với Client
1. Kết nối với server
- Sau khi bạn đã chọn vai trò của mình là client, màn hình form client sẽ hiển thị và bước 1 này cũng chính là bước QUAN TRỌNG cho các bước sau. Trước khi có thể xem được stream từ phía server, bạn phải nhập địa chỉ IP của server sau đó nhấn nút OK. Nếu hiển thị "Kết nối đến server thành công" thì bạn đã thành công, còn nếu không hãy xem lại địa chỉ IP bạn đã nhập
2. Bắt đầu xem stream
- Sau khi đã kết nối đến server thành công, bạn hãy nhấn nút "Bắt đầu xem", client sẽ bắt đầu nhận dữ liệu truyền đến từ server (màn hình stream và âm thanh).
3. Chỉnh âm lượng
- Sau khi đã hoàn thành chức năng 1 và chức năng 2, chức năng điều chỉnh âm lượng là tùy chọn, bạn có thể kéo thả để tùy chọn âm lượng mình muốn (lớn hay nhỏ).

Hiện tại, chương trình vẫn còn đơn giản và nhiều lỗi chưa chỉnh sửa, mong các bạn thông cảm, sẽ sớm khắc phục về các đoạn lỗi

Dây là link QC (cho đến hiện tại): https://docs.google.com/document/d/1cKoe9TDMmxg-MRWQeY1CK0-nUCn3TJU0/edit?usp=sharing&ouid=111789487383099014956&rtpof=true&sd=true
"""
