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

## Tìm đường đi ngắn nhất

Cuối cùng là tìm đường đi ngắn nhất dựa trên ma trận `path` :

Ví dụ
```
điểm bắt đầu là xp = 1
điểm kết thúc là kt = 7
```

> dựa theo dòng 0 (xp - 1  = 0) có 3 đỉnh 2 5 6 khác -1 là 3 đỉnh mà đỉnh 1 tới đỉnh 7 chắc chắn phải đi qua

ta đưa vào `stack` -> 2 | 5 | 6 | 7

> Kiểm tra

trước hết tạo danh sách đường đi : `list<int>`

thêm phần tử đầu tiên : `list.add(xp)`

Lập lần 1
```
xp = 1 - 1 = 0;
kt = st.pop() = 2;
[xp, kt - 1] = -1 (đúng vì không có đỉnh nào chen giữa đỉnh 1 và 2)
-> thêm đỉnh : 2
cập nhật lại : xp = kt = 2;
```

Lập lần 2
```
xp = 2 - 1 = 1;
kt = st.pop() = 5;
[xp, kt - 1] = -1 (đúng vì không có đỉnh nào chen giữa đỉnh 2 và 5)
-> thêm đỉnh : 5
cập nhật lại : xp = kt = 5;
```

Lập lần 3
```
xp = 5 - 1 = 4;
kt = st.pop() = 6;
[xp, kt - 1] = -1 (đúng vì không có đỉnh nào chen giữa đỉnh 5 và 6)
-> thêm đỉnh : 6
cập nhật lại : xp = kt = 6;
```

Lập lần 4
```
xp = 6 - 1 = 5;
kt = st.pop() = 7;
[xp, kt - 1] = 4 (Xuất hiện đỉnh chen giữa đỉnh 6 và 7)

-> thêm đỉnh : 4
thêm lại phần tử kt (7) vào stack (nếu không thì stack sẽ rỗng và bỏ qua phần tử này)

cập nhật lại : xp = [xp, kt - 1] = 4;
```

Lập lần 5
```
xp = 4 - 1 = 3;
kt = st.pop() = 7;
[xp, kt - 1] = -1 (đúng vì không có đỉnh nào chen giữa đỉnh 4 và 7)
-> thêm đỉnh : 7
cập nhật lại : xp = kt = 3;
```

lúc này `stack` rỗng nên dừng vòng lặp.

Xuất danh sách ta sẽ đc : 1 2 5 6 4 7

Kết thúc.
---