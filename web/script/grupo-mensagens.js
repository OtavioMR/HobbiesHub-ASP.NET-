document.addEventListener('DOMContentLoaded', () => {
    const grupoId = getGrupoIdFromURL();

    if (grupoId) {
        loadMessages(grupoId);
    } else {
        alert('Grupo não encontrado.');
    }

    document.getElementById('sendMessageButton').addEventListener('click', () => sendMessage(grupoId));
});

function getGrupoIdFromURL() {
    const params = new URLSearchParams(window.location.search);
    return params.get('grupoId');
}

async function loadMessages(grupoId) {
    try {
        const response = await fetch(`https://localhost:44327/api/Chat/getMessages?groupId=${grupoId}`);
        if (!response.ok) {
            throw new Error('Erro ao carregar mensagens');
        }
        const mensagens = await response.json();
        displayMessages(mensagens);
    } catch (error) {
        console.error('Erro ao carregar mensagens:', error);
        alert('Ocorreu um erro ao carregar as mensagens.');
    }
}

function displayMessages(mensagens) {
    const messagesContainer = document.getElementById('messages');
    messagesContainer.innerHTML = '';

    mensagens.forEach(msg => {
        const messageItem = document.createElement('div');
        messageItem.className = 'message-item';
        messageItem.innerHTML = `
            <p><strong>${msg.userId || 'Desconhecido'}:</strong> ${msg.texto || 'Sem mensagem'}</p>
            <span class="text-muted">${msg.hora ? new Date(msg.hora).toLocaleString() : 'Hora desconhecida'}</span>
        `;
        messagesContainer.appendChild(messageItem);
    });
}

async function sendMessage(grupoId) {
    const messageInput = document.getElementById('messageInput');
    const texto = messageInput.value.trim();
    const userId = 'usuario-logado-id'; // Substitua pelo ID do usuário logado

    if (!texto) {
        alert('Mensagem não pode ser vazia.');
        return;
    }

    const message = {
        GroupId: grupoId,
        UserId: userId,
        Texto: texto,
        Hora: new Date().toISOString()
    };

    console.log("Dados da mensagem a serem enviados:", JSON.stringify(message));

    try {
        const response = await fetch('https://localhost:44327/api/Chat/sendMessage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(message)
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.error('Detalhes do erro:', errorData);
            alert('Erro ao enviar mensagem. Detalhes: ' + JSON.stringify(errorData.errors));
            throw new Error('Erro ao enviar mensagem');
        }

        loadMessages(grupoId); // Recarregar as mensagens para incluir a nova mensagem
        messageInput.value = ''; // Limpar o campo de entrada
    } catch (error) {
        console.error('Erro ao enviar mensagem:', error);
        alert('Ocorreu um erro ao enviar a mensagem.');
    }
}
