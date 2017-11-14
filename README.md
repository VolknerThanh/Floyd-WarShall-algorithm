# Floyd-WarShall-algorithm

#### Thuật toán tìm đường đi ngắn nhất Floyd-WarShall

## Hướng giải

> 1. Tạo ma trận và chuyển dữ liệu file từ danh sách kề sang ma trận kề.
> 2. Tạo ma trận `int[,] dist` để lưu lại sự thay đổi của trọng số trong ma trận kề.
> 3. Tạo ma trận `int[,] path` để lưu lại các đỉnh mà mỗi đỉnh sẽ đi qua

## Giải thích code

`ts = 10000` đây là biến trọng số, giá trị có thể cho là vô cùng hoặc số nào lớn để không lẩn với các giá trị khác trong ma trận.

hàm `Create()` sẽ khởi tạo ma trận kề có giá trị bằng 10000 hoặc vô cùng như đã nói ở trên.

hàm `CreateOtherMatrix()` là hàm khởi tạo các ma trận `dist` và `path`. Đồng thời, ta gán lại giá trị sau khi thay đổi trọng số cho ma trận kề.

> tại sao không kết hợp 2 hàm `Create()` và hàm `CreateOtherMatrix()` ?

**Vì nếu ta kết hợp 2 hàm, ma trận sẽ được khởi tạo với giá trị của tất cả các phần tử đều bằng nhau (10000). Sau khi ta thay đổi trọng số, ta gán lại thì tất cả các phần tử đều có giá trị y hệt nhau => bug**

> tại sao không gán trọng số bằng `int.MaxValue` mà phải cho gán số khác ?

**Vì `int.MaxValue` cộng cho 1 số nào đó thì nó sẽ quay về âm => bug**

