# Floyd-WarShall-algorithm

#### Thuật toán tìm đường đi ngắn nhất Floyd-WarShall

## Hướng giải

> 1. Tạo ma trận và chuyển dữ liệu file từ danh sách kề sang ma trận kề.
> 2. Tạo ma trận `int[,] dist` để lưu lại sự thay đổi của trọng số trong ma trận kề.
> 3. Tạo ma trận `int[,] path` để lưu lại các đỉnh mà mỗi đỉnh sẽ đi qua

## Giải thích code

`ts = 10000` đây là biến trọng số, giá trị có thể cho là vô cùng hoặc số nào lớn để không lẩn với các giá trị khác trong ma trận.

hàm `Creatr()` sẽ khởi tạo ma trận kề có giá trị bằng 10000 hoặc vô cùng như đã nói ở trên.

