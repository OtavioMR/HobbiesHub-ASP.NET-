function getAllusuarios() {
    // Faz uma requisição GET para a API de produtos
    axios.get('https://localhost:44325/api/Usuario')
        .then((response) => {
            const usuarios = response.data; // O vetor de objetos de produtos é armazenado aqui
            console.log(usuarios); // Para ver os dados retornados no console

            const usuarioList = document.querySelector("#all-usuarios-slick"); 
            usuarioList.innerHTML = ""; // Limpa qualquer conteúdo existente

            // Itera sobre cada produto no vetor de produtos
            usuarios.forEach((usuario) => {
                const usuarioElement = document.createElement("div");
                usuarioElement.className = "usuario"; // Define a classe para o elemento do produto

                // Cria a estrutura HTML do produto
                usuarioElement.innerHTML = `
                    <div class="usuario-img">
                        <img src="./img/${usuario.usuarioImage}" alt="">
                    </div>
                    <div class="usuario-body">
                        <p class="usuario-category">${usuario.usuarioCategory}</p>
                        <h3 class="usuario-name"><a href="#">${usuario.usuarioName}</a></h3>
                        <h4 class="usuario-price">R$${usuario.usuarioPrice} <del class="usuario-old-price">R$${usuario.usuarioPrice}</del></h4>
                        <div class="usuario-rating">
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star"></i>
                            <i class="fa fa-star"></i>
                        </div>
                        <div class="usuario-btns">
                            <button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>
                            <button class="add-to-compare"><i class="fa fa-exchange"></i><span class="tooltipp">add to compare</span></button>
                            <button class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>
                        </div>
                    </div>
                    <div class="add-to-cart">
                        <button class="add-to-cart-btn"><i class="fa fa-shopping-cart"></i> add to cart</button>
                    </div>
                `;

                // Re-inicializa Slick Slider
$('.products-slick').slick('unslick'); // Destroy any slick slider before initializing it again
$('.products-slick').slick({
    slidesToShow: 4,
    slidesToScroll: 1,
    autoplay: true,
    infinite: true,
    speed: 300,
    dots: false,
    arrows: true,
    appendArrows: $(productList).attr('data-nav'),
    responsive: [
        {
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        }
    ]
})
.catch((error) => console.error('Error loading products:', error));

document.addEventListener('DOMContentLoaded', function() {
    getAllProducts();
});})}
