CREATE TABLE usr001_user (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    username VARCHAR(255) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    role INTEGER NOT null,
    password TEXT NOT NULL,
    password_salt TEXT NOT NULL
);

CREATE TABLE phr001_phrase (
    id BIGSERIAL PRIMARY KEY,
    description TEXT NOT NULL,
    author TEXT NOT NULL,
    is_visible BOOLEAN NOT NULL DEFAULT true,
    created_by BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL,
    updated_by BIGINT NOT NULL,
    updated_at TIMESTAMP NOT NULL,
    CONSTRAINT fk_created_by FOREIGN KEY (created_by) REFERENCES usr001_user (id) ON DELETE RESTRICT,
    CONSTRAINT fk_updated_by FOREIGN KEY (updated_by) REFERENCES usr001_user (id) ON DELETE RESTRICT
);
