document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('formCadastro');

    if (!form) {
        console.error('O formulário não foi encontrado. Verifique o ID "formCadastro".');
        return;
    }

    form.addEventListener('submit', async (event) => {
        event.preventDefault();

        // Obtendo valores do formulário
        const nomeCompleto = document.getElementById('usuarioName').value.trim();
        const nomeUsuario = document.getElementById('usuarioNameSystem').value.trim();
        const email = document.getElementById('usuarioEmail').value.trim();
        const senha = document.getElementById('usuarioSenha').value.trim();
        const confirmarSenha = document.getElementById('usuarioSenhaConfirmar').value.trim();
        const dataNascimento = document.getElementById('dateOfBirth').value;

        // Validações básicas
        if (!nomeCompleto || !nomeUsuario || !email || !senha || !confirmarSenha || !dataNascimento) {
            alert('Por favor, preencha todos os campos.');
            return;
        }

        if (senha !== confirmarSenha) {
            alert('As senhas não coincidem!');
            return;
        }

        const usuario = {
            NameUsuario: nomeCompleto,
            NameSystemUsuario: nomeUsuario,
            EmailUsuario: email,
            SenhaUsuario: senha,
            DateOfBirth: new Date(dataNascimento).toISOString()
        };

        console.log('Payload enviado:', usuario);

        try {
            const response = await fetch('https://localhost:44327/api/Usuario/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(usuario)
            });

            if (response.ok) {
                alert('Cadastro realizado com sucesso!');
                window.location.href = './login.html'; // Redireciona após o cadastro bem-sucedido
            } else {
                const errorData = await response.json();
                console.error('Erro retornado pela API:', errorData);

                if (errorData.errors) {
                    let mensagemErro = 'Erro ao cadastrar usuário:\n';
                    for (const [campo, mensagens] of Object.entries(errorData.errors)) {
                        mensagemErro += `${campo}: ${mensagens.join(', ')}\n`;
                    }
                    alert(mensagemErro);
                } else {
                    alert(`Erro ao cadastrar usuário: ${errorData.title || 'Erro desconhecido'}`);
                }
            }
        } catch (error) {
            console.error('Erro:', error);
            alert('Ocorreu um erro ao tentar cadastrar o usuário.');
        }
    });
});
