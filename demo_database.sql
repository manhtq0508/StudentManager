CREATE TABLE LOPHOC (
    MALOP CHAR(10) NOT NULL PRIMARY KEY,
    TEN VARCHAR(50)
);

INSERT INTO LOPHOC (MALOP, TEN) VALUES
    ('IT001', 'Lớp gì đó 1'),
    ('IT002', 'Lớp gì đó 2'),
    ('IT003', 'Lớp gì đó 3'),
    ('IT004', 'Lớp gì đó 4');

CREATE TABLE HOCSINH (
    MSSV CHAR(10) NOT NULL PRIMARY KEY,
    TEN VARCHAR(50),
    NGSINH VARCHAR(30),
    MALOP VARCHAR(10) NOT NULL,
    FOREIGN KEY (MALOP) REFERENCES LOPHOC(MALOP) 
);

INSERT INTO HOCSINH (MSSV, TEN, NGSINH, MALOP) VALUES
    ('23520001', 'Học sinh gì đó', '2020-01-21', 'IT001'),
    ('23520002', 'Học sinh gì đó', '2020-02-01', 'IT001'),
    ('23520003', 'Học sinh gì đó', '2020-12-29', 'IT001'),
    ('23520004', 'Học sinh gì đó', '2020-05-11', 'IT001'),
    ('23520005', 'Học sinh gì đó', '2020-02-25', 'IT001'),
    
    ('23520006', 'Học sinh gì đó', '2020-02-06', 'IT002'),
    ('23520007', 'Học sinh gì đó', '2020-03-04', 'IT002'),
    ('23520008', 'Học sinh gì đó', '2020-02-06', 'IT002'),
    ('23520009', 'Học sinh gì đó', '2020-11-20', 'IT002'),
    
    ('23520010', 'Học sinh gì đó', '2020-01-30', 'IT003'),
    ('23520011', 'Học sinh gì đó', '2020-07-22', 'IT003'),
    ('23520012', 'Học sinh gì đó', '2020-08-12', 'IT003'),
    ('23520013', 'Học sinh gì đó', '2020-12-13', 'IT003'),
    
    ('23520014', 'Học sinh gì đó', '2020-12-01', 'IT004'),
    ('23520015', 'Học sinh gì đó', '2020-09-02', 'IT004'),
    ('23520016', 'Học sinh gì đó', '2020-04-30', 'IT004'),
    ('23520017', 'Học sinh gì đó', '2020-02-01', 'IT004'),
    ('23520018', 'Học sinh gì đó', '2020-09-01', 'IT004');
