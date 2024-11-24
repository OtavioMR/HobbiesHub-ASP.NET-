document.getElementById('createGroupButton').addEventListener('click', checkLoginStatus);
document.getElementById('joinGroupButton').addEventListener('click', checkLoginStatus);
document.getElementById('logoutButton').addEventListener('click', logout);

async function checkLoginStatus(event) {
    const action = event.target.id === 'createGroupButton' ? 'criar' : 'entrar';
    const token = localStorage.getItem('token'); // Pega o token JWT armazenado no login

    if (!token) {
        alert('Você precisa fazer login para continuar.');
        window.location.href = './login.html'; // Redireciona para a página de login
        return;
    }

    try {
        const response = await fetch('https://localhost:44327/api/Account/IsLoggedIn', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            const data = await response.json();
            if (data.loggedIn) {
                alert(`Você pode ${action} um grupo.`);
                // Aqui você pode redirecionar o usuário ou executar outras ações
            } else {
                alert('Você precisa fazer login para continuar.');
                window.location.href = './login.html'; // Redireciona para a página de login
            }
        } else {
            alert('Ocorreu um erro ao verificar o status de login.');
        }
    } catch (error) {
        console.error('Erro:', error);
        alert('Ocorreu um erro ao verificar o status de login.');
    }
}

function logout() {
    localStorage.removeItem('token');
    alert('Você saiu da conta.');
    window.location.href = './login.html'; // Redireciona para a página de login
}
