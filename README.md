CMPE232 – Tracking and Optimizing Milk Production and Distribution
Course: CMPE 232 – Database Systems, TED University
Group: TED CowCode Innovators
Semester: Fall 2024

📌 Project Overview
This project delivers a comprehensive milk supply chain tracking and optimization system. It manages every stage — from farm collection to retail shelves — ensuring freshness, quality assurance, and operational efficiency.

The system leverages:

Real-time IoT monitoring for temperature & humidity

Automated quality control workflows

Inventory and order management

Route optimization for shipments

Alerts for quality deviations or delivery issues

🗄 Database Design
Our database is 3NF normalized for integrity and performance.
It models both core entities and complex structures to capture the real-world dynamics of the milk industry.

Main Entities
Entity	Description
Products	Milk product details (name, brand, flavor, pack size, price)
Inventory	Stock levels per warehouse location
Suppliers	Supplier information and linked products
Orders	Customer purchase details
Customers	Customer profiles and loyalty points
Shipments	Delivery tracking for each order
Employees	Staff managing operations

Complex Structures
Weak Entity: PromotionalOffers linked to Products

Aggregation: OrderDetails linking Orders and Products

Hierarchy: Staff as a subtype of Employees

Key Relationships
mermaid
Copy
Edit
erDiagram
    PRODUCTS ||--|| INVENTORY : "1:1"
    PRODUCTS }o--o{ SUPPLIERS : "M:N"
    ORDERS }o--|| CUSTOMERS : "M:1"
    ORDERS }o--o{ PRODUCTS : "M:N (via OrderDetails)"
    SHIPMENTS ||--|| ORDERS : "1:1"
    EMPLOYEES ||--o{ ORDERS : "1:N"
⚙️ Features
Real-Time Monitoring – Track temperature & humidity during transport

Quality Control – Automated checks at farm, transit, and retail stages

Inventory Management – Real-time stock updates across warehouses

Order & Shipment Tracking – End-to-end traceability

Route Optimization – Shortest delivery paths with minimal cost

Alerts – Instant notifications for anomalies

🖥 UI Components
Login & Registration – Role-based access control

Admin Dashboard – Key metrics & notifications

Inventory Management – Update and monitor stock

Quality Control Panel – Inspection logs & alerts

Order Management – Process and view orders

Shipment Tracking – Real-time route visualization

Customer Portal – Order history, loyalty points, tracking

🛠 Technologies Used
Layer	Technology
Database	MySQL (3NF, triggers, views, constraints)
Backend	C# / .NET
Frontend	HTML, CSS
IoT Integration	Sensor-based monitoring (temperature & humidity)
Version Control	Git / GitHub

📂 Example Queries
1️⃣ View Product Summary with Stock & Supplier Info

CREATE VIEW ProductSummaryView AS
SELECT p.ProductID, p.Name, p.Brand, i.QuantityAvailable, s.Name AS SupplierName
FROM Products p
LEFT JOIN Inventory i ON p.ProductID = i.ProductID
LEFT JOIN SupplierProducts sp ON p.ProductID = sp.ProductID
LEFT JOIN Suppliers s ON sp.SupplierID = s.SupplierID;
2️⃣ Track Orders by Customer

SELECT o.OrderID, c.Name, o.OrderDate, o.TotalAmount
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID
WHERE c.CustomerID = 101;
3️⃣ Automatic Inventory Update after Orders

CREATE TRIGGER UpdateInventoryAfterOrder
AFTER INSERT ON OrderDetails
FOR EACH ROW
BEGIN
    UPDATE Inventory
    SET QuantityAvailable = QuantityAvailable - NEW.Quantity
    WHERE ProductID = NEW.ProductID;
END;

📚 References
Ataseven, Z. Y. (2023). Süt ve Süt Ürünleri Durum Tahmin Raporu 2023.

Chen, C., Zhang, J., & Delaurentis, T. (2013). Quality control in food supply chain management.

Xiu, C., & Klein, K. (2010). Melamine in milk products in China.
