import ProfileImg from "../assets/icons/profile_img.png";
import ImageIcon from "../assets/icons/imagen.svg";

export function EditProfileCard() {
  const submitHandler = e => {
    e.preventDefault();
    console.log("funciona");
  };

  return (
    <form
      className="p-4 bg-white flex gap-4 border border-slate-500 rounded-2xl w-[40em]"
      method="post"
      enctype="multipart/form-data"
    >
      <div className="relative">
        <div className="">
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
        <div className="flex flex-col gap-4" method="POST">
          <label className="flex flex-col gap-2" htmlFor="name">
            <h3 className="text-black">Nombre(s)</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white"
            name="name"
            type="text"
          />

          <label className="flex flex-col gap-2" htmlFor="name">
            <h3 className="text-black">Apellido(s)</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white"
            name="name"
            type="text"
          />

          <label className="flex flex-col gap-2" htmlFor="desc">
            <h3 className="text-black">Descripción</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white"
            name="desc"
            type="text"
          />
          <label className="flex flex-col gap-2" htmlFor="location">
            <h3 className="text-black">Ubicación</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white"
            name="location"
            type="text"
          />

          <label className="flex flex-col gap-2">
            <h3 className="text-black">Contraseña</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white"
            type="password"
            minLength={5}
            maxLength={15}
          />
          <button
            className="font-bold p-2 border text-terciary-100 border-terciary-100 rounded-2xl w-[10em] m-auto hover:bg-cyan-100 transition-colors"
            type="submit"
            onClick={submitHandler}
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
