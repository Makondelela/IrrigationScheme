-- Run against the irrigation_management database after the schema has been created.
-- The fixed UUIDs make this script safe to run repeatedly.

CREATE TABLE IF NOT EXISTS irrigation_schemes (
    id uuid PRIMARY KEY,
    name varchar(200) NOT NULL,
    river_name varchar(200) NOT NULL,
    description varchar(1000) NOT NULL
);

CREATE TABLE IF NOT EXISTS villages (
    id uuid PRIMARY KEY,
    name varchar(200) NOT NULL,
    "Order" integer NOT NULL,
    irrigation_scheme_id uuid NOT NULL REFERENCES irrigation_schemes(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS sub_canals (
    id uuid PRIMARY KEY,
    name varchar(200) NOT NULL,
    village_id uuid NOT NULL REFERENCES villages(id) ON DELETE CASCADE,
    number_of_farmers integer NOT NULL
);

CREATE TABLE IF NOT EXISTS farmers (
    id uuid PRIMARY KEY,
    name varchar(200) NOT NULL,
    village_id uuid NOT NULL REFERENCES villages(id) ON DELETE CASCADE,
    sub_canal_id uuid NOT NULL REFERENCES sub_canals(id) ON DELETE RESTRICT
);

INSERT INTO irrigation_schemes (id, name, river_name, description)
VALUES ('10000000-0000-0000-0000-000000000001', 'Mutale Irrigation Scheme', 'Mutale River', 'Shared canal supplying five villages')
ON CONFLICT (id) DO NOTHING;

INSERT INTO villages (id, name, "Order", irrigation_scheme_id)
VALUES
    ('20000000-0000-0000-0000-000000000001', 'Matangari', 1, '10000000-0000-0000-0000-000000000001'),
    ('20000000-0000-0000-0000-000000000002', 'Tshiombo', 2, '10000000-0000-0000-0000-000000000001'),
    ('20000000-0000-0000-0000-000000000003', 'Mianzwi', 3, '10000000-0000-0000-0000-000000000001'),
    ('20000000-0000-0000-0000-000000000004', 'Maraxwe', 4, '10000000-0000-0000-0000-000000000001'),
    ('20000000-0000-0000-0000-000000000005', 'Mbahela', 5, '10000000-0000-0000-0000-000000000001'),
    ('20000000-0000-0000-0000-000000000006', 'Muhotoni', 6, '10000000-0000-0000-0000-000000000001'),
    ('20000000-0000-0000-0000-000000000007', 'Mutshenzheni', 7, '10000000-0000-0000-0000-000000000001')
ON CONFLICT (id) DO UPDATE SET
    name = EXCLUDED.name,
    "Order" = EXCLUDED."Order",
    irrigation_scheme_id = EXCLUDED.irrigation_scheme_id;

INSERT INTO sub_canals (id, name, village_id, number_of_farmers)
VALUES
    ('30000000-0000-0000-0000-000000000001', 'A1', '20000000-0000-0000-0000-000000000001', 2),
    ('30000000-0000-0000-0000-000000000002', 'A2', '20000000-0000-0000-0000-000000000002', 1)
ON CONFLICT (id) DO NOTHING;

INSERT INTO farmers (id, name, village_id, sub_canal_id)
VALUES
    ('40000000-0000-0000-0000-000000000001', 'Farmer 1', '20000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000001'),
    ('40000000-0000-0000-0000-000000000002', 'Farmer 2', '20000000-0000-0000-0000-000000000001', '30000000-0000-0000-0000-000000000001'),
    ('40000000-0000-0000-0000-000000000003', 'Farmer 3', '20000000-0000-0000-0000-000000000002', '30000000-0000-0000-0000-000000000002')
ON CONFLICT (id) DO NOTHING;
