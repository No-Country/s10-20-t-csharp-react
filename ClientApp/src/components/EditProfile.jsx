import ProfileImg from "../assets/icons/profile_img.png";

export function EditProfile() {
  return (
    <article className="backdrop-blur-sm w-full h-screen z-4 top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 absolute">
      <img
        className="rounded-full"
        width="80"
        height="80"
        src={ProfileImg}
        alt="Foto de perfil"
      />
      <div>
        <form className="flex flex-col gap-4" method="POST">
          <label htmlFor="name">
            <h3>Nombre Completo</h3>
            <input
              className="p-2"
              name="name"
              type="text"
              value="Nombre Completo"
            />
          </label>
          <label htmlFor="desc">
            <h3>Descripción</h3>
            <input
              className="p-2"
              name="desc"
              type="text"
              value="Descripción"
            />
          </label>
          <label htmlFor="location">
            <h3>Ubicación</h3>
            <input
              className="p-2"
              name="location"
              type="text"
              value="Descripción"
            />
          </label>
          <label htmlFor="email">
            <h3>Email</h3>
            <input className="p-2" name="email" type="email" value="" />
          </label>
          <label>
            <h3>Contraseña</h3>
            <input
              className="p-2"
              type="password"
              minLength={5}
              maxLength={15}
              value=""
            />
          </label>
          <button type="submit">Guardar</button>
        </form>
        <div className="flex flex-col gap-4">
          <button className="text-red-800">
            Desactivar temporalmente mi cuenta
          </button>
          <button className="text-red-800">Eliminar mi cuenta</button>
        </div>
      </div>
    </article>
  );
}
