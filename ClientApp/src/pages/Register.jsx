import React from "react";
import { Link } from "react-router-dom";
import { MainLayout } from "../layout/MainLayout";

const Register = () => {
  return (
    <MainLayout>
      <div>
        <>
          <div className="flex mt-12 justify-center">
            <div className="text-center justify-center">
              <h1 className="font-bold text-xl">Bienvenidos a QuejLAP </h1>
              <img src="https://img.freepik.com/vector-gratis/vector-degradado-logotipo-colorido-pajaro_343694-1365.jpg"></img>
            </div>
            <main className="flex justify-end  items-center  gap-10 sm:gap-0 my-6 mx-2 sm:mx-5 md:mx-10">
              <div className="basis-1/2 w-[50vh] flex flex-1 flex-col sm:gap-0 justify-center p-6 py-8 sm:py-6 lg:px-8 rounded-lg bg-gray-200 bg-opacity-60 shadow-md">
                <h2 className="text-center text-2xl font-PoppinsBold leading-9 tracking-tight text-pallete-black">
                  Crear Cuenta
                </h2>

                <form className="w-full" action="#">
                  <div className="flex flex-col gap-6 sm:gap-4 mt-9 sm:mx-auto sm:w-full sm:max-w-sm">
                    <div>
                      <div className="mt-2">
                        <input
                          id="user"
                          name="user"
                          placeholder="Nombre"
                          type="text"
                          required
                          className="input input-sm block w-full border border-black font-PoppinsRegular 
                                 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                        />
                      </div>
                    </div>

                    <div>
                      <div className="mt-2">
                        <input
                          id="user"
                          name="user"
                          placeholder="Apellido"
                          type="text"
                          required
                          className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                        />
                      </div>
                    </div>

                    <div>
                      <div className="mt-2">
                        <input
                          id="residence"
                          name="residence"
                          placeholder="Lugar de Nacimiento"
                          type="text"
                          required
                          className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                        />
                      </div>
                    </div>

                    <div>
                      <div className="mt-2">
                        <input
                          id="location"
                          name="location"
                          placeholder="Localidad de Residencia"
                          type="text"
                          autoComplete="location"
                          required
                          className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                        />
                      </div>
                    </div>

                    <div>
                      <div className="mt-2">
                        <input
                          id="date"
                          name="date"
                          placeholder="Fecha de nacimiento"
                          type="text"
                          required
                          className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                        />
                      </div>
                    </div>

                    <div className="mt-12">
                      <div className="mt-2">
                        <input
                          id="email"
                          name="email"
                          placeholder="Email"
                          type="Email"
                          required
                          className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                        />
                      </div>
                    </div>

                    <div className="">
                      <div className="">
                        <input
                          id="password"
                          name="password"
                          placeholder="Contraseña"
                          type="password"
                          required
                          className="input input-sm block w-full border border-black font-PoppinsRegular 
                                ring-pallete-grey focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                        />
                        <p className="mt-2 text-sm text-gray-500">
                          La contraseña debe contener al menos
                        </p>
                        <ul>
                          <li className=" text-xs">. Una Mayúscula</li>
                          <li className=" text-xs">. Un número</li>
                          <li className=" text-xs">. 8 Caracteres</li>
                        </ul>
                      </div>
                    </div>

                    <button
                      type="submit"
                      className="btn btn-md px-10 mt-4 border border-black "
                    >
                      Registrarse
                    </button>

                    <div className="flex flex-col gap-3 mt-5 mx-auto items-center justify-center">
                      <p className=" text-center text-xs sm:text-sm font-PoppinsSemibold text-pallete-grey">
                        ¿Ya estás registrado?
                        <Link
                          Link
                          to="/login"
                          className="px-2 font-PoppinsSemibold leading-6 text-pallete-green"
                        >
                          Iniciar sesión
                        </Link>
                      </p>
                    </div>
                  </div>
                </form>
              </div>
            </main>
          </div>
        </>
      </div>
    </MainLayout>
  );
};

export default Register;
