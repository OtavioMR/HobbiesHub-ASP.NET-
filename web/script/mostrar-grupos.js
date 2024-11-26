document.addEventListener('DOMContentLoaded', fetchGrupos);

async function fetchGrupos() {
    try {
        const response = await fetch('https://localhost:44327/api/Grupo');
        if (!response.ok) {
            throw new Error('Erro ao buscar grupos');
        }
        const grupos = await response.json();
        displayGrupos(grupos);
    } catch (error) {
        console.error('Erro ao carregar grupos:', error);
        alert('Ocorreu um erro ao buscar os grupos.');
    }
}

function displayGrupos(grupos) {
    const groupForm = document.getElementById('group-form');
    groupForm.innerHTML = ''; // Limpar o conteúdo anterior

    grupos.forEach(grupo => {
        const groupItem = document.createElement('div');
        groupItem.className = 'grupo-item';
        groupItem.innerHTML = `
            <div class="group-card">
                <h3>${grupo.nomeGrupo}</h3>
                <p>${grupo.descricaoGrupo}</p>
                <span class="timestamp">Criado em: ${new Date(grupo.dataCriacao).toLocaleDateString()}</span>
            </div>
        `;
        // Adiciona um evento de clique a cada grupo
        groupItem.addEventListener('click', () => {
            window.location.href = `grupo-mensagens.html?grupoId=${grupo.id}`;
        });
        groupForm.appendChild(groupItem);
    });
}
