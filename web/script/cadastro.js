document.addEventListener('DOMContentLoaded', function() {
    document.getElementById('formCadastro').addEventListener('submit', async function(event) {
      event.preventDefault();
      const formData = new FormData(event.target);
      const data = {
        nameUsuario: formData.get('usuarioName'),
        nameSystemUsuario: formData.get('usuarioNameSystem'),
        emailUsuario: formData.get('usuarioEmail'),
        senhaUsuario: formData.get('usuarioSenha'),
        dateOfBirth: formData.get('dateOfBirth')
      };
  
      try {
        const response = await fetch('https://localhost:44327/api/Usuario', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(data)
        });
  
        if (response.ok) {
          console.log('Usuário cadastrado com sucesso!');
        } else {
          const errorMessage = await response.text();
          console.log('Erro ao cadastrar usuário:', response.status, response.statusText, errorMessage);
        }
      } catch (error) {
        console.log('Erro ao cadastrar usuário:', error);
      }
    });
  });
  