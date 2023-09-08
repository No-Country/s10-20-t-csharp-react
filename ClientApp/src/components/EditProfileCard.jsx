import ProfileImg from "../assets/icons/profile_img.png";
import ImageIcon from "../assets/icons/imagen.svg";

export function EditProfileCard() {
  return (
    <form
      className="p-4 bg-white flex gap-4 border border-slate-500 rounded-2xl w-[40em]"
      method="post"
      enctype="multipart/form-data"
    >
      <div className="relative">
        <div>
          <img
            className="rounded-full"
            src={ProfileImg}
            alt="Foto de perfil"
            width={80}
            height={80}
          />
          <img
            className="absolute top-2 left-2"
            src={ImageIcon}
            alt="Image Icon"
            width={50}
            height={50}
          />
          <label htmlFor="image_upload">Upload</label>
          <input
            id="image_upload"
            name="image_upload"
            className="absolute top-2 left-2 file:border-0 file:bg-transparent file:opacity-0"
            type="file"
            accept=".jpg, .jpeg, .png"
          />
        </div>
      </div>
      <div className=" border-slate-400 p-4 flex flex-col gap-10 w-full">
        <div className="flex flex-col gap-8" method="POST">
          <label className="flex flex-col gap-2" htmlFor="name">
            <h3>Nombre(s)</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md"
            name="name"
            type="text"
          />

          <label className="flex flex-col gap-2" htmlFor="name">
            <h3>Apellido(s)</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md"
            name="name"
            type="text"
          />

          <label className="flex flex-col gap-2" htmlFor="desc">
            <h3>Descripción</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md"
            name="desc"
            type="text"
          />
          <label className="flex flex-col gap-2" htmlFor="location">
            <h3>Ubicación</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md"
            name="location"
            type="text"
          />

          <label className="flex flex-col gap-2">
            <h3>Contraseña</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md"
            type="password"
            minLength={5}
            maxLength={15}
          />
          <button
            className="font-bold p-2 border text-terciary-100 border-terciary-100 rounded-2xl w-[10em] m-auto hover:bg-cyan-100 transition-colors"
            type="submit"
          >
            Guardar
          </button>
        </div>
        <div className="flex flex-col gap-4">
          <button className="text-red-800">
            <strong>Desactivar temporalmente mi cuenta</strong>
          </button>
          <button className="text-red-800">
            <strong>Eliminar mi cuenta</strong>
          </button>
        </div>
      </div>
    </form>
  );
}
