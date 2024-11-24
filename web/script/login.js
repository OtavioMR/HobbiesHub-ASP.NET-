document.addEventListener('DOMContentLoaded', function () {
    console.log("Página carregada. O script foi executado.");

    const loginForm = document.getElementById('loginForm');  // Acessando o formulário

    // Verificando se o formulário foi carregado corretamente
    if (loginForm) {
        console.log("Formulário encontrado!");

        loginForm.addEventListener('submit', async function (event) {
            event.preventDefault();  // Evitar envio do formulário e recarregamento da página

            // Capturando os valores dos campos de email e senha
            const email = document.getElementById('email').value;
            const password = document.getElementById('password').value;

            console.log("Email: ", email);  // Logando email no console
            console.log("Password: ", password);  // Logando senha no console

            // Verificando se os campos não estão vazios
            if (!email || !password) {
                alert('Por favor, preencha todos os campos!');
                return;
            }

            // Dados que serão enviados para o servidor
            const credentials = {
                Email: email,
                Password: password
            };

            try {
                const response = await fetch('https://localhost:44327/api/Auth/login', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(credentials)
                });

                if (response.ok) {
                    const data = await response.json();
                    console.log("Token recebido:", data.token);  // Logando o token recebido no console

                    // Armazenando o token no localStorage
                    localStorage.setItem('token', data.token);
                    console.log('Token armazenado:', localStorage.getItem('token'));

                    // Redirecionando após o login bem-sucedido
                    alert('Login bem-sucedido!');
                    window.location.href = './index.html';  // Ou qualquer outra página desejada
                } else {
                    const errorData = await response.text();
                    alert(`Erro ao fazer login: ${errorData}`);
                }
            } catch (error) {
                console.error('Erro:', error);
                alert('Ocorreu um erro ao tentar fazer login.');
            }
        });
    } else {
        console.log("Formulário não encontrado.");
    }
});
