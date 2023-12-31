import { useEffect, useState } from "react";
import axios from "axios";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";

const Modal = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [isOpen, setIsOpen] = useState(false);
  const navigate = useNavigate();

  const token = localStorage.getItem("userSession");

  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "multipart/form-data",
    },
  };

  const onSubmit = data => {
    console.log(data);
    axios
      .post("https://s10nc.somee.com/api/quejas/queja2", data, config)
      .then(res => {
        alert("Publicación generada");
        console.log("Respuesta API: ", res);
        navigate("/muro").catch(err => console.error(err));
      })
      .catch(err => {
        console.error(err);
        alert("Hubo un error al generar un reporte");
      });
  };

  return (
    <div
      className={`absolute z-10 top-0 left-1/2 backdrop-blur-sm w-20 flex flex-col gap-4 items-center justify-center bg-slate-50 shadow-2xl shadow-side-left ${
        isOpen ? "hidden" : "flex"
      } `}
    >
      <form
        className="flex flex-col gap-4 bg-white items-center w-fit p-10 rounded-xl"
        onSubmit={handleSubmit(onSubmit)}
      >
        <div
          className="w-full mt-5 bg-teal-500 relative cursor-pointer"
          onClick={() => setIsOpen(!isOpen)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            height="1em"
            viewBox="0 0 384 512"
            className="absolute z-10 right-0 mr-4 -mt-4"
          >
            <path d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z" />
          </svg>
        </div>
        <div className="w-full">
          <label htmlFor="title" className="text-black">
            Título:
          </label>
          <input
            type="text"
            name="title"
            id="title"
            className="p-2 w-full rounded-xl border-black border-[1.8px] bg-white"
            {...register("Title", { required: true })}
          />
        </div>
        <div className="w-full relative">
          {/*<label htmlFor="#" className="absolute mt-2 pl-2 font-medium">Categoría</label>*/}
          <input
            type="number"
            name="category_ID"
            id="category_ID"
            className="p-2 w-full rounded-xl bg-white border-black border-[1.8px] placeholder:text-black"
            placeholder="Categoría"
            {...register("Category_ID", {
              required: true,
              setValueAs: v => parseInt(v),
            })}
          />
        </div>
        <div className="w-full relative">
          {/*<label htmlFor="#" className="absolute mt-2 pl-2 font-extralight">Descripción</label>*/}
          <textarea
            rows={10}
            cols={55}
            className="rounded-xl bg-white border-black border-[1.5px] p-2 placeholder:text-black"
            placeholder="Descripción"
            name="text"
            {...register("Text", { required: true })}
          />
        </div>
        <div className="w-full">
          <label htmlFor="district_Name" className="absolute p-2 mt-1">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              height="1em"
              viewBox="0 0 384 512"
            >
              <path d="M215.7 499.2C267 435 384 279.4 384 192C384 86 298 0 192 0S0 86 0 192c0 87.4 117 243 168.3 307.2c12.3 15.3 35.1 15.3 47.4 0zM192 128a64 64 0 1 1 0 128 64 64 0 1 1 0-128z" />
            </svg>
          </label>
          <input
            type="text"
            name="district_Name"
            id="district_Name"
            className="p-2 w-full rounded-xl bg-white border-black border-[1.8px]"
            {...register("district_Name", { required: true })}
          />
        </div>

        <div className="w-full">
          <p className="text-black font-bold">Subir Fotos</p>
          <input
            id="media"
            name="media"
            className="file:border-terciary-100 file:p-2 file:rounded-md file:bg-white"
            type="file"
            accept=".jpg, .jpeg, .png"
            {...register("media")}
          />
        </div>

        <div className="flex flex-col gap-2 w-full">
          <div className="flex gap-4 justify-between text-start text-black">
            <p>¿Desea que su queja sea anónima?</p>
            <input
              type="checkbox"
              name="anonymous"
              id="anonymous"
              className="bg-white p-2 rounded-md"
            />
          </div>

          <div className="flex gap-4 justify-between text-black">
            <Link className="underline text-terciary-100" to="/terms">
              Acepto los términos y condiciones de convivencia
            </Link>
            <input
              type="checkbox"
              name="terms"
              id="terms"
              className="bg-white p-2 rounded-md"
              required
            />
          </div>
        </div>

        <button
          type="submit"
          className="font-bold p-2 bg-terciary-100 hover:bg-terciary-50 text-white w-full rounded-lg mt-4"
        >
          Publicar
        </button>
      </form>
    </div>
  );
};

export default Modal;
