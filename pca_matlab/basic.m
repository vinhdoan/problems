% Tạo một tập dữ liệu hai chiều ngẫu nhiên, trong đó độ thay đổi trên một
% chiều gấp 10 lần chiều kia:
a = 10*randn(1000,1);
b = randn(1000,1);
% Vẽ đồ thị tập dữ liệu ban đầu:
plot(a,b,'.');
% Thực hiện một phép xoay (ví dụ 75 độ) để lấy một tập dữ liệu mới:
R = [cosd(75) -sind(75); sind(75) cosd(75)];
G = R*[a,b]';
x = G(1,:);
y = G(2,:);
% Vẽ đồ thị tập dữ liệu này (cùng với đồ thị tập dữ liệu cũ):
hold on;
plot(x,y,'g.');
% Tính PCA:
coeff = pca(G');
% Véc tơ chỉ phương đặc trưng nhất của tập dữ liệu:
e = coeff(:,1);
% Biểu diễn véc tơ này trên cùng đồ thị, với gốc véc tơ là trung tâm của
% tập dữ liệu:
start = mean(G,2);
x_start = start(1);
y_start = start(2);
x_length = 20*e(1);
y_length = 20*e(2);
quiver(x_start,y_start,x_length,y_length,0);