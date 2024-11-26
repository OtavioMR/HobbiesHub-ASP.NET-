document.addEventListener('DOMContentLoaded', () => {
    loadMessages();
    initializeRealTimeUpdates();
});

function getGrupoIdFromURL() {
    const params = new URLSearchParams(window.location.search);
    return params.get('grupoId');
}

async function loadMessages() {
    const grupoId = getGrupoIdFromURL();
    if (!grupoId) {
        alert('Grupo não encontrado.');
        return;
    }

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
    messagesContainer.innerHTML = ''; // Limpar mensagens anteriores

    mensagens.forEach(msg => {
        const messageItem = document.createElement('div');
        messageItem.className = msg.userId === "usuario-logado-id" ? 'message sent' : 'message received';
        messageItem.innerHTML = `
            <p>${msg.texto}</p>
            <span class="time">${new Date(msg.hora).toLocaleTimeString()}</span>
        `;
        messagesContainer.appendChild(messageItem);
    });
}

async function sendMessage() {
    const grupoId = getGrupoIdFromURL();
    const messageInput = document.getElementById('messageInput');
    const texto = messageInput.value.trim();
    const userId = "usuario-logado-id"; // Substitua pelo ID real do usuário logado

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

    try {
        const response = await fetch('https://localhost:44327/api/Chat/sendMessage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(message)
        });

        if (!response.ok) {
            throw new Error('Erro ao enviar mensagem');
        }

        // Adicionar a mensagem enviada à tela
        loadMessages(); // Recarregar as mensagens para incluir a nova mensagem
        messageInput.value = ''; // Limpar o campo de entrada
    } catch (error) {
        console.error('Erro ao enviar mensagem:', error);
        alert('Ocorreu um erro ao enviar a mensagem.');
    }
}

document.getElementById('sendMessageButton').addEventListener('click', sendMessage);

function initializeRealTimeUpdates() {
    const grupoId = getGrupoIdFromURL();
    const socket = new WebSocket(`wss://localhost:44327/api/Chat/realtime?groupId=${grupoId}`);

    socket.onmessage = function(event) {
        const message = JSON.parse(event.data);
        displayMessages([message]);
    };

    socket.onerror = function(error) {
        console.error('WebSocket error:', error);
    };
}
