import { useContext, useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import axios from "axios";

import { UserContext } from "../store/userContext";
import ImageIcon from "../assets/icons/imagen.svg";

export function EditProfileCard() {
  const { register, handleSubmit } = useForm();
  const navigate = useNavigate();

  const userContext = useContext(UserContext);

  return (
    <form
      className="p-4 sm:p-2 bg-white flex sm:flex-col sm:items-center gap-4 border border-slate-500 rounded-2xl w-[40em] sm:w-[20em]"
      method="post"
      encType="multipart/form-data"
      onSubmit={handleSubmit(data => {
        console.log(data);
        axios
          .put("https://s10nc.somee.com/api/me", data)
          .then(res => {
            console.log(res.status);
            alert("Datos cambiados");
          })
          .catch(error => {
            console.log(error);
            alert("Hubo un error");
          })
          .finally(() => navigate("/profile"));
      })}
    >
      <div className="relative">
        <div className="">
          <img
            className="rounded-full"
            src={userContext.userProfileImage}
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
            className="w-10 absolute top-2 left-2 file:border-0 file:bg-transparent file:opacity-0 file:text-transparent"
            type="file"
            accept=".jpg, .jpeg, .png"
          />
        </div>
      </div>
      <div className=" border-slate-400 p-4 flex flex-col gap-10 w-full">
        <div className="flex flex-col gap-4" method="POST">
          <label className="flex flex-col gap-2" htmlFor="name">
            <h3 className="text-black font-bold">Nombre(s)</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white text-black"
            name="givenName"
            type="text"
            {...register("givenName")}
          />

          <label className="flex flex-col gap-2" htmlFor="name">
            <h3 className="text-black font-bold">Apellido(s)</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white text-black"
            name="lastname"
            type="text"
            {...register("name")}
          />

          <label className="flex flex-col gap-2" htmlFor="desc">
            <h3 className="text-black font-bold">Descripción</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white text-black"
            name="desc"
            type="text"
            {...register("description")}
          />
          <label className="flex flex-col gap-2" htmlFor="location">
            <h3 className="text-black font-bold">Ubicación</h3>
          </label>
          <input
            className="p-2 border border-slate-400 rounded-md bg-white text-black"
            name="address"
            type="text"
            {...register("address")}
          />

          <label className="flex flex-col gap-2">
            <h3 className="text-black font-bold">Contraseña</h3>
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
