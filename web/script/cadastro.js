document.getElementById('formCadastro').addEventListener('submit', async (event) => {
  event.preventDefault();

  const usuarioName = document.getElementById('usuarioName').value;
  const usuarioNameSystem = document.getElementById('usuarioNameSystem').value;
  const usuarioEmail = document.getElementById('usuarioEmail').value;
  const usuarioSenha = document.getElementById('usuarioSenha').value;
  const usuarioSenhaConfirmar = document.getElementById('usuarioSenhaConfirmar').value;
  const dateOfBirth = document.getElementById('dateOfBirth').value;

  if (usuarioSenha !== usuarioSenhaConfirmar) {
      alert('As senhas não coincidem. Por favor, verifique e tente novamente.');
      return;
  }

  const usuario = {
      NameUsuario: usuarioName,
      NameSystemUsuario: usuarioNameSystem,
      EmailUsuario: usuarioEmail,
      SenhaUsuario: usuarioSenha,
      DateOfBirth: dateOfBirth
  };

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
          window.location.href = './login.html'; // Redireciona para a página de login após o cadastro bem-sucedido
      } else {
          const errorData = await response.json();
          alert(`Erro ao cadastrar usuário: ${errorData.message}`);
      }
  } catch (error) {
      console.error('Erro:', error);
      alert('Ocorreu um erro ao tentar cadastrar o usuário.');
  }
});
