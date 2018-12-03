using SextaDoPastel.Dominio;
using SextaDoPastel.Model;
using SextaDoPastel.Negocio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SextaDoPastel.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private LoginModel _loginNegocio;

        /// <summary>
        /// 
        /// </summary>
        public LoginController()
        {
            _loginNegocio = new LoginModel();
        }

        /// <summary>
        /// MÉTODO QUE OBTÉM UMA LISTA DOS "LOGINS"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Login), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_loginNegocio.Selecionar());
        }

        /// <summary>
        /// MÉTODO QUE OBTÉM UM "LOGIN" POR {ID}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Login), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_loginNegocio.SelecionarPorId(id));
        }

        
        /// <summary>
        /// MÉTODO QUE OBTÉM UM "LOGIN" POR {EMAIL}
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Email/{email}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Login), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetEmail(string email)
        {
            return Ok(_loginNegocio.SelecionarPorEmail(email));
        }//*/

        /// <summary>
        /// MÉTODO QUE OBTÉM UM "LOGIN" POR {NICK}
        /// </summary>
        /// <param name="nick"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Nick/{nick}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Login), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetNick(string nick)
        {
            return Ok(_loginNegocio.SelecionarPorNick(nick));
        }

        /// <summary>
        /// MÉTODO QUE VALIDA O LOGIN POR ({CPF} / {EMAIL}) E {SENHA}
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Validar")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Login), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetLogin([FromQuery]string login, [FromQuery]string senha)
        {
            return Ok(_loginNegocio.EfetuarLogin(login, senha));
        }

        /// <summary>
        /// MÉTODO QUE INSERE UM "LOGIN"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Login), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]LoginInput input)
        {
            var objLogin = new Login()
            {
                EMAIL = input.EMAIL,
                NICK = input.NICK,
                SENHA = input.SENHA
            };

            var idLogin = _loginNegocio.Inserir(objLogin);
            objLogin.ID_LOG = idLogin;
            return CreatedAtRoute(nameof(GetId), new { id = idLogin }, objLogin);
        }

        /// <summary>
        /// MÉTODO QUE ALTERA SENHA DO "LOGIN" POR {ID}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("pass/{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Login), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]LoginInput input)
        {
            var objLogin = new Login()
            {
                SENHA = input.SENHA
            };

            var obj = _loginNegocio.AlterarSenha(id, objLogin);
            return Accepted(obj);
        }

        /// <summary>
        /// MÉTODO QUE ALTERA SENHA DO "LOGIN" POR {ID}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("NickEmail/{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Login), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult PutEmailNick([FromRoute]int id, [FromBody]LoginInput input)
        {
            var objLogin = new Login()
            {
                EMAIL = input.EMAIL,
                NICK = input.NICK
            };

            var obj = _loginNegocio.AlterarEmailNick(id, objLogin);
            return Accepted(obj);
        }

        /// <summary>
        /// MÉTODO QUE ALTERA STATUS ATIVO/INATIVO POR {ID}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("status/{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Login), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult PutStatus([FromRoute]int id, [FromBody]LoginInput input)
        {
            var objLogin = new Login()
            {
                STATUS = input.STATUS
            };

            var obj = _loginNegocio.AlterarStatus(id, objLogin);
            return Accepted(obj);
        }

        /// <summary>
        /// MÉTODO QUE ALTERA PERFIL USER/ADM POR {ID}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("perfil/{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Login), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult PutPerfil([FromRoute]int id, [FromBody]LoginInput input)
        {
            var objLogin = new Login()
            {
                PERFIL = input.PERFIL
            };

            var obj = _loginNegocio.AlterarPerfil(id, objLogin);
            return Accepted(obj);
        }

        /// <summary>
        /// MÉTODO QUE EXCLUI UM "LOGIN" POR {ID}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _loginNegocio.Deletar(id);
            return Ok();
        }
    }
}
