import { Link } from "react-router-dom";
import { MainLayout } from "../layout/MainLayout";
import { LeftArrow } from "../components/atoms/leftArrow";

export function Terms() {
  return (
    <MainLayout>
      <section className="py-20">
        <Link
          to="/report"
          className="flex gap-4 items-center text-terciary-100 m-auto"
        >
          Ir atrás <LeftArrow />
        </Link>
        <article className="m-auto flex flex-col gap-4 w-[70ch]">
          <h1 className="font-bold text-2xl text-center">
            Términos y Condiciones del Servicio
          </h1>

          <h2 className="font-semibold text-left">1. Introducción</h2>
          <p>
            Bienvenido a <strong>RedCo.</strong>, un servicio en línea que te
            proporciona acceso a [descripción de los servicios o productos que
            ofreces]. Al utilizar nuestro sitio web, aceptas cumplir con estos
            términos y condiciones, así como con nuestra Política de Privacidad.
            Si no estás de acuerdo con alguno de estos términos, por favor, no
            utilices nuestros servicios.
          </p>

          <h2 className="font-semibold text-left">2. Uso Aceptable</h2>
          <p>
            Debes utilizar nuestros servicios de manera ética y legal. No debes
            usarlos para actividades ilegales, difamatorias, ofensivas o que
            violen los derechos de terceros.
          </p>

          <h2 className="font-semibold text-left">3. Registro de Cuenta</h2>
          <p>
            Para acceder a ciertas áreas de nuestro sitio, es posible que debas
            registrarte y crear una cuenta. Debes proporcionar información
            precisa y actualizada durante el proceso de registro y mantener la
            confidencialidad de tus credenciales de inicio de sesión.
          </p>

          <h2 className="font-semibold text-left">4. Privacidad</h2>
          <p>
            Respetamos tu privacidad y protegemos tus datos personales de
            acuerdo con nuestra Política de Privacidad. Al utilizar nuestros
            servicios, aceptas nuestras prácticas de recopilación y uso de
            datos.
          </p>

          <h2 className="font-semibold text-left">5. Propiedad Intelectual</h2>
          <p>
            Todos los contenidos presentes en este sitio web, incluyendo pero no
            limitado a texto, gráficos, logotipos, imágenes y software, están
            protegidos por derechos de autor y otras leyes de propiedad
            intelectual. No tienes permiso para copiar, distribuir, modificar o
            usar estos contenidos sin autorización.
          </p>

          <h2 className="font-semibold text-left">
            6. Limitación de Responsabilidad
          </h2>
          <p>
            No nos hacemos responsables de ningún daño directo o indirecto que
            puedas sufrir al utilizar nuestros servicios. Te recomendamos que
            siempre actúes de manera responsable y que tomes las medidas
            necesarias para proteger tus datos y dispositivos.
          </p>

          <h2 className="font-semibold text-left">7. Modificaciones</h2>
          <p>
            Nos reservamos el derecho de modificar estos términos y condiciones
            en cualquier momento. Las modificaciones entrarán en vigor
            inmediatamente después de su publicación en nuestro sitio web. Es tu
            responsabilidad revisar periódicamente estos términos para estar al
            tanto de los cambios.
          </p>

          <h2 className="font-semibold text-left">8. Contacto</h2>
          <p>
            Si tienes preguntas o comentarios sobre estos términos y
            condiciones, por favor, contáctanos en [correo electrónico de
            contacto].
          </p>
        </article>
      </section>
    </MainLayout>
  );
}
