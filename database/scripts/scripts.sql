CREATE TABLE usr001_user (
    id BIGSERIAL PRIMARY KEY, -- ID como chave primária e auto-incremento
    name VARCHAR(255) NOT NULL, -- Nome do usuário
    username VARCHAR(255) NOT NULL UNIQUE, -- Nome de usuário único
    email VARCHAR(255) NOT NULL UNIQUE, -- E-mail único
    role INTEGER NOT null,
    password TEXT NOT NULL, -- Senha do usuário
    password_salt TEXT NOT NULL -- Sal da senha
);

CREATE TABLE phr001_phrase (
    id BIGSERIAL PRIMARY KEY,                 -- Identificador único da frase
    description TEXT NOT NULL,             -- Descrição da frase
    author TEXT NOT NULL,                  -- Autor da frase
    is_visible BOOLEAN NOT NULL DEFAULT true,
    created_by BIGINT NOT NULL,            -- ID do usuário que criou o registro
    created_at TIMESTAMP NOT NULL,         -- Data de criação do registro
    updated_by BIGINT NOT NULL,            -- ID do usuário que atualizou o registro
    updated_at TIMESTAMP NOT NULL,         -- Data da última atualização
    CONSTRAINT fk_created_by FOREIGN KEY (created_by) REFERENCES usr001_user (id) ON DELETE RESTRICT,
    CONSTRAINT fk_updated_by FOREIGN KEY (updated_by) REFERENCES usr001_user (id) ON DELETE RESTRICT
);
