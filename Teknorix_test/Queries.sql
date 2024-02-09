
CREATE DATABASE teknorix_test;

-- Create the database and then run the following queries all togeather
USE teknorix_test;

-- Create the 'countries' table 
CREATE TABLE countries (
    country_id INT IDENTITY(1, 1) PRIMARY KEY,
    country_name VARCHAR(30) UNIQUE NOT NULL
);

-- Insert some example countries (Records will be created by the admin)
INSERT INTO countries (country_name) VALUES ('India');
INSERT INTO countries (country_name) VALUES ('USA');
INSERT INTO countries (country_name) VALUES ('Australia');

-- Create the 'states' table with a foreign key reference to 'countries'
CREATE TABLE states (
    state_id INT IDENTITY(1, 1) PRIMARY KEY,
    state_name VARCHAR(50) NOT NULL,
    country_id INT NOT NULL FOREIGN KEY REFERENCES countries(country_id),
	CONSTRAINT UQ_StateName_CountryID UNIQUE (state_name, country_id)
);
 
-- Insert individual rows into the 'states' table (Records will be created by the admin)
INSERT INTO states (state_name, country_id) VALUES ('California', 2); 
INSERT INTO states (state_name, country_id) VALUES ('Karnataka', 1);  
INSERT INTO states (state_name, country_id) VALUES ('New South Wales', 3);  


-- Create the 'departments' table 
CREATE TABLE departments (
    department_id INT IDENTITY(1, 1) PRIMARY KEY,
    department_name VARCHAR(50) NOT NULL
);

-- Insert individual rows into the 'departments' table (Records will be created by the admin, but we can also allow clients to create them if it is really necessary, depending on the business logic or the client can make a request to add a department)
INSERT INTO departments (department_name) VALUES ('Finance');
INSERT INTO departments (department_name) VALUES ('Human Resources');
INSERT INTO departments (department_name) VALUES ('Marketing');


-- Create the 'companies' table
CREATE TABLE companies (
    company_id INT IDENTITY(1, 1) PRIMARY KEY,
    company_name VARCHAR(30) UNIQUE NOT NULL,
    industry VARCHAR(20),
    contact_email VARCHAR(50) UNIQUE NOT NULL,
    phone_number VARCHAR(20),
    registration_date DATE NOT NULL
); 

-- Insert individual rows into the 'companies' table  (Records will be created by the clients)
INSERT INTO companies (company_name, industry, contact_email, phone_number, registration_date)
VALUES 
    ('ABC Corporation', 'Technology', 'contact@abccorp.com', '+1 (123) 456-7890', '2024-02-09'),
    ('XYZ Ltd', 'Finance', 'info@xyzltd.com', '+44 20 1234 5678', '2023-11-15'),
    ('Tech Innovators', 'IT Services', 'info@techinnovators.com', '+91 9876543210', '2022-08-30');

 -- Create the 'locations' table (Branches would be a more appropriate name for the logic that is implemented here)
CREATE TABLE company_locations (
    company_location_id INT IDENTITY(1, 1) PRIMARY KEY,
    title VARCHAR(30) NOT NULL,
    city VARCHAR(30) NOT NULL,
	zip VARCHAR(10) NOT NULL, 
    state_id INT NOT NULL,  --foreign key relationship with the 'countries' table
	company_id INT NOT NULL,
    FOREIGN KEY (state_id) REFERENCES states(state_id),
	FOREIGN KEY (company_id) REFERENCES companies(company_id)
);
 
-- Insert individual rows into the 'locations' table (Records about locations will be created by the client as part of the company profile. When posting a job, the client will be able to select the location from a dropdown menu in the form. )
INSERT INTO company_locations (title, city, zip, state_id, company_id) VALUES ('Headquarters', 'Los Angeles','ZQ222', 1, 1);
INSERT INTO company_locations (title, city, zip, state_id, company_id) VALUES ('Branch Office', 'Bangalore','PD002', 2, 2);
INSERT INTO company_locations (title, city, zip, state_id, company_id) VALUES ('Regional Office', 'Sydney', 'KO199', 3, 3);  

-- Create the 'jobs' table
CREATE TABLE jobs (
    job_id INT IDENTITY(1, 1) PRIMARY KEY,
    department_id INT,  --foreign key relationship with the 'departments' table
    company_location_id INT NOT NULL,  --foreign key relationship with the 'company_locations' table
    job_code VARCHAR(20) NOT NULL,
    title VARCHAR(100) NOT NULL,
    description TEXT NOT NULL,
	posted_date DATE NOT NULL,
    closing_date DATE NOT NULL,
    FOREIGN KEY (department_id) REFERENCES departments(department_id),
    FOREIGN KEY (company_location_id) REFERENCES company_locations(company_location_id)
); 


-- Insert individual rows into the 'jobs' table (Records will be created by the clients)
INSERT INTO jobs (posted_date, closing_date, department_id, company_location_id, job_code, title, description)
VALUES 
    ('2024-02-09', '2024-03-09', 1, 1, 'IT001', 'Software Developer', 'We are looking for a skilled Software Developer with experience in web application development.'),
    ('2024-01-15', '2024-02-28', 2, 2, 'FIN002', 'Financial Analyst', 'Join our finance team as a Financial Analyst and contribute to financial planning and analysis.'),
	('2023-11-30', '2024-01-15', 3, 3, 'MKT003', 'Marketing Specialist', 'Exciting opportunity for a Marketing Specialist to develop and execute marketing strategies.'),
    ('2024-02-12', '2024-03-20', 3, 2, 'MKT014', 'Content Writer', 'We are seeking a talented Content Writer to create engaging and informative content for various platforms.'),
    ('2024-01-25', '2024-03-01', 1, 1, 'IT015', 'Systems Analyst', 'Join our IT team as a Systems Analyst to analyze and improve our information systems and processes.'),
	('2023-12-15', '2024-02-01', 2, 3, 'HR016', 'Recruitment Specialist', 'Exciting opportunity for a Recruitment Specialist to identify and attract top talent to our organization.'),
    ('2024-01-10', '2024-02-25', 3, 2, 'ENG017', 'Aerospace Engineer', 'We are hiring an Aerospace Engineer to contribute to the design and development of aerospace systems.'),
    ('2023-11-30', '2024-01-15', 3, 1, 'MKT018', 'Digital Marketing Manager', 'Join our Marketing team as a Digital Marketing Manager and lead digital campaigns to enhance brand awareness.'),
    ('2024-02-05', '2024-03-15', 1, 2, 'IT019', 'UX/UI Designer', 'Exciting opportunity for a UX/UI Designer to create user-friendly and visually appealing interfaces for our applications.'),
    ('2024-01-18', '2024-03-01', 2, 1, 'FIN020', 'Tax Accountant', 'Join our finance team as a Tax Accountant and ensure compliance with tax regulations and reporting requirements.'),
    ('2023-12-08', '2024-01-20', 3, 3, 'ENG021', 'Structural Engineer', 'We are looking for a Structural Engineer to design and analyze structures for various construction projects.'),
    ('2024-01-28', '2024-03-05', 3, 2, 'MKT022', 'Brand Manager', 'Exciting opportunity for a Brand Manager to develop and implement strategies to enhance our brand image.'),
    ('2023-11-15', '2024-01-01', 1, 1, 'IT023', 'Network Security Specialist', 'Join our IT security team as a Network Security Specialist to safeguard our network infrastructure.'),
    ('2024-02-01', '2024-03-15', 3, 1, 'ENG004', 'Mechanical Engineer', 'We are hiring a skilled Mechanical Engineer to design and analyze mechanical systems.'),
    ('2024-01-20', '2024-02-28', 2, 2, 'HR005', 'Human Resources Coordinator', 'Join our HR team as a Coordinator and contribute to employee relations and engagement.'),
	('2023-12-10', '2024-01-25', 1, 3, 'IT006', 'Network Administrator', 'Exciting opportunity for a Network Administrator to manage and optimize network infrastructure.'),
    ('2024-01-05', '2024-02-15', 3, 1, 'MKT007', 'Social Media Manager', 'We are looking for an experienced Social Media Manager to handle our social media presence.'),
    ('2023-11-25', '2024-01-10', 3, 2, 'ENG008', 'Civil Engineer', 'Join our Civil Engineering team and contribute to the design and construction of infrastructure projects.'),
    ('2024-02-02', '2024-03-10', 2, 3, 'FIN009', 'Accounting Manager', 'Exciting opportunity for an Accounting Manager to oversee financial reporting and analysis.'),
    ('2024-01-12', '2024-02-28', 3, 1, 'MKT010', 'Event Coordinator', 'Join our Marketing team as an Event Coordinator and organize impactful events for the company.'),
    ('2023-12-05', '2024-01-20', 1, 2, 'IT011', 'Database Administrator', 'We are hiring a skilled Database Administrator to manage and optimize database systems.'),
    ('2024-01-30', '2024-03-05', 3, 3, 'ENG012', 'Electrical Engineer', 'Exciting opportunity for an Electrical Engineer to design and implement electrical systems.'),
    ('2023-11-20', '2024-01-05', 2, 1, 'FIN013', 'Investment Analyst', 'Join our finance team as an Investment Analyst and analyze market trends and investment opportunities.');


	 