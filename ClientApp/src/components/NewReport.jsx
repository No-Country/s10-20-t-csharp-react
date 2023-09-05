import cancelIcon from "../assets/icons/cancelar.svg";

export function NewReport({ isOpen, setIsOpen }) {
  return (
    <div className="backdrop-blur-sm w-full h-screen z-4 top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 absolute">
      <form className="border bg-white flex flex-col gap-4 p-14 items-center rounded-2xl fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2">
        <img
          className="absolute right-2 top-2 cursor-pointer"
          src={cancelIcon}
          alt="Icono de cancel"
          width="30"
          height="30"
          onClick={() => setIsOpen(!isOpen)}
        />
        <label htmlFor="title" className="w-full">
          <p>Título:</p>
          <input
            className="border rounded-md p-2 w-full"
            name="title"
            type="text"
            required
          />
        </label>
        <div className="w-full">
          <select className="border rounded-md p-2 w-full bg-white" required>
            <option>Categoría</option>
            <option>Option 2</option>
            <option>Option 3</option>
            <option>Option 4</option>
          </select>
        </div>
        <label htmlFor="description" className="w-full">
          <p>Descripción:</p>
          <textarea
            className="border rounded-md p-2 w-full resize-none"
            name="description"
            minLength="15"
            maxLength="150"
            required
          />
        </label>
        <label htmlFor="location" className="w-full">
          <p>Ubicación:</p>
          <input
            className="border rounded-md p-2 w-full"
            name="location"
            type="text"
            required
          />
        </label>
        <label htmlFor="images">
          <p>Subir fotos y vídeos</p>
          <input name="images" className="p-2" type="file" required multiple />
        </label>
        <label
          htmlFor="anonymous-report"
          className="flex justify-between w-full"
        >
          <p>¿Desea que su reporte sea anónimo?</p>
          <input name="anonymous-report" type="checkbox" required />
        </label>
        <label
          htmlFor="terms-conditions"
          className="flex justify-between w-full"
        >
          <p>
            Acepto los{" "}
            <a name="terms-conditions" className="text-terciary-100" href="#">
              términos y condiciones
            </a>
          </p>
          <input type="checkbox" />
        </label>
        <button className="bg-terciary-100 p-2 text-white rounded-2xl w-full">
          Publicar reporte
        </button>
      </form>
    </div>
  );
}
