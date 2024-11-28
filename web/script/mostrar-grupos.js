document.addEventListener('DOMContentLoaded', function () {
    console.log("Página carregada. O script foi executado.");

    const gruposList = document.getElementById('grupos-list');

    if (gruposList) {
        console.log("Container de grupos encontrado!");
        fetchGrupos();
    } else {
        console.log("Container de grupos não encontrado.");
    }

    async function fetchGrupos() {
        try {
            console.log("Iniciando requisição para a API de grupos...");
            const response = await fetch('https://localhost:44327/api/Grupo?timestamp=' + new Date().getTime());

            if (!response.ok) {
                throw new Error('Erro ao buscar grupos. Status: ' + response.status);
            }

            const grupos = await response.json();
            console.log("Dados recebidos da API:", grupos);
            displayGrupos(grupos);
        } catch (error) {
            console.error('Erro ao carregar grupos:', error);
            alert('Ocorreu um erro ao buscar os grupos.');
        }
    }

    function displayGrupos(grupos) {
        if (!grupos || grupos.length === 0) {
            gruposList.innerHTML = '<p class="text-center">Nenhum grupo disponível.</p>';
            return;
        }

        gruposList.innerHTML = '';
        grupos.forEach(grupo => {
            const groupItem = document.createElement('div');
            groupItem.className = 'grupo-item';
            groupItem.innerHTML = `
                <h3>${grupo.nomeGrupo}</h3>
                <p>${grupo.descricaoGrupo}</p>
                <span class="text-muted">Criado em: ${new Date(grupo.dataCriacao).toLocaleDateString()}</span>
            `;
            groupItem.style.cursor = 'pointer';

            groupItem.addEventListener('click', () => {
                window.location.href = `grupo-mensagens.html?grupoId=${grupo.id}`;
            });

            gruposList.appendChild(groupItem);
        });
    }
});
