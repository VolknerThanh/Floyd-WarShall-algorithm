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

**- Vì nếu ta kết hợp 2 hàm, ma trận sẽ được khởi tạo với giá trị của tất cả các phần tử đều bằng nhau (10000). Sau khi ta thay đổi trọng số, ta gán lại thì tất cả các phần tử đều có giá trị y hệt nhau => bug**

> tại sao không gán trọng số bằng `int.MaxValue` mà phải cho gán số khác ?

**- Vì `int.MaxValue` cộng cho 1 số nào đó thì nó sẽ quay về âm => bug**

> Ví dụ :

giải thuật floyd-warshall:

```csharp
for (int k = 0; k < n; k++)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (dist[i, j] > dist[k, j] + dist[i, k])
            {
                dist[i, j] = dist[k, j] + dist[i, k];
                path[i, j] = k + 1;
            }
        }
    }
}
```
|0|7|*8*|16|inf|inf|inf|
|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
|*7*|0|`inf`|inf|3|6|inf|
|8|inf|0|inf|inf|inf|inf|
|16|inf|inf|0|inf|2|4|
|inf|3|inf|inf|0|2|inf|
|inf|6|inf|2|2|0|inf|
|inf|inf|inf|4|inf|inf|0|

nó sẽ tìm `matrix[1,2] > matrix[0,2] + matrix[1,0]`, nếu đúng thì `matrix[1,2] = matrix[0,2] + matrix[1,0]`

với hình thì `matrix[1,2]` là `inf`, `matrix[0,2]` là 8 và `matrix[1,0]` là 7

|0|7|*8*|16|inf|inf|inf|
|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
|*7*|0|`15`|inf|3|6|inf|
|8|inf|0|inf|inf|inf|inf|
|16|inf|inf|0|inf|2|4|
|inf|3|inf|inf|0|2|inf|
|inf|6|inf|2|2|0|inf|
|inf|inf|inf|4|inf|inf|0|

tương tự những phần tử khác...

*Ta lặp lại n lần (n là số lượng đỉnh) với vòng lặp `k = 0; k < n; k++`*

> lúc này ma trận path cũng cập nhật lại vị trí `k`. Lưu ý, `k` là *đỉnh* chứ không phải cạnh hay trọng số !!!


## Đồ thị chạy minh họa

Đây là đồ thị gốc : 

|0|7|8|16|inf|inf|inf|
|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
|7|0|inf|inf|3|6|inf|
|8|inf|0|inf|inf|inf|inf|
|16|inf|inf|0|inf|2|4|
|inf|3|inf|inf|0|2|inf|
|inf|6|inf|2|2|0|inf|
|inf|inf|inf|4|inf|inf|0|

Sau khi ta chạy hết vòng lặp, ta được ma trận dist như sau : 

|0|7|8|14|10|12|18|
|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
|7|0|15|7|3|5|11|
|8|15|0|22|18|20|26|
|14|7|22|0|4|2|4|
|10|3|18|4|0|2|8|
|12|5|20|2|2|0|6|
|18|11|26|4|8|6|0|

Và ma trận đường đi (qua các đỉnh) : *muốn đi qua đỉnh thứ ... thì phải qua các đỉnh này*

|-1|-1|-1|6|2|5|6|
|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
|-1|-1|1|6|-1|5|6|
|-1|1|-1|6|2|5|6|
|6|6|6|-1|6|-1|-1|
|2|-1|2|6|-1|-1|6|
|5|5|5|-1|-1|-1|4|
|6|6|6|-1|6|4|-1|