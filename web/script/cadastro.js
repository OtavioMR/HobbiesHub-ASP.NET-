

document.getElementById('formCadastro').addEventListener('submit', async function (event) {
    event.preventDefault();  // Previne o envio tradicional do formulário

    // Coletando os dados do formulário
    const nome = document.getElementById('usuarioName').value;
    const usuario = document.getElementById('usuarioNameSystem').value;
    const email = document.getElementById('usuarioEmail').value;
    const senha = document.getElementById('usuarioSenha').value;
    const senhaConfirmar = document.getElementById('usuarioSenhaConfirmar').value;

    // Verificação das senhas
    if (senha !== senhaConfirmar) {
        alert('As senhas não coincidem!');
        return;
    }

    // Criando objeto com os dados do usuário
    const userData = {
        UsuarioName: nome,  // Altera o campo "nome" para "UsuarioName"
        UsuarioNameSystem: usuario,  // Altera para "UsuarioNameSystem"
        UsuarioEmail: email,  // Altera para "UsuarioEmail"
        UsuarioSenhaHash: senha,  // Idealmente, você deveria criptografar a senha antes de enviar
        UsuarioDateCadastro: birthdate  // Altere isso para um formato compatível com o backend
    };
    

    try {
        // Enviando os dados via POST para a API
        const response = await fetch('https://localhost:44325/api/Usuario', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userData),
        });

        // Tratando a resposta da API
        if (response.ok) {
            alert('Usuário cadastrado com sucesso!');
        } else {
            alert('Erro ao cadastrar usuário.');
        }
    } catch (error) {
        console.error('Erro na requisição:', error);
        alert('Erro de rede. Tente novamente.');
    }
});
