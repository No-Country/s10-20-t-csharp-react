import React from "react";
import { Link } from "react-router-dom";
import { MainLayout } from "../layout/MainLayout";
import axios from "axios";
import { useState, useEffect } from "react";
import imageFon from "../images/imageFon.png";
import imageFonTwo from "../images/img_myreports.png";

const Register = () => {
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [birthdatePlace, setBirthdatePlace] = useState("");
  const [residence, setResidence] = useState("");
  const [birthdate, setBirthdate] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const registerNewUser = () => {
    const newUserToBeRegistered = {
      email,
    };
    axios
      .post("https://s10nc.somee.com/api/register", newUserToBeRegistered)
      .then(res => {
        console.log(res.data);
      })
      .catch(err => {
        console.log(err);
      });
  };

  return (
    <MainLayout>
      <>
        <div className="flex mt-12 gap-14 justify-center">
          <main className="flex justify-end  items-center w-[30em] gap-10 sm:gap-0 my-6 mx-2 sm:mx-5 md:mx-10">
            <div className="basis-1/2 w-[50vh] flex flex-1 flex-col sm:gap-0 justify-center p-6 py-8 sm:py-6 lg:px-8 rounded-lg bg-white bg-opacity-60 shadow-md">
              <h2 className="text-center text-2xl font-bold tracking-tight text-black">
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
                        className="p-2 text-black block w-full border rounded-xl border-black font-regular bg-white sm:text-sm sm:leading-6"
                        onChange={e => setName(e.target.value)}
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
                        className="p-2 text-black block w-full border rounded-xl border-black font-regular bg-white sm:text-sm sm:leading-6"
                        onChange={e => setSurname(e.target.value)}
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
                        className="block text-black w-full border rounded-xl border-black font-regular bg-white sm:text-sm sm:leading-6"
                        onChange={e => setBirthdatePlace(e.target.value)}
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
                        className="p-2 text-black block w-full border rounded-xl border-black font-regular bg-white sm:text-sm sm:leading-6"
                        onChange={e => setResidence(e.target.value)}
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
                        className="p-2 text-black block w-full border rounded-xl border-black font-regular bg-white sm:text-sm sm:leading-6"
                        onChange={e => setBirthdate(e.target.value)}
                      />
                    </div>
                  </div>

                  <div className="mt-12">
                    <div className="mt-2">
                      <input
                        id="email"
                        name="email"
                        placeholder="Email"
                        type="email"
                        required
                        className="p-2 text-black block w-full border rounded-xl border-black font-regular bg-white sm:text-sm sm:leading-6"
                        onChange={e => setEmail(e.target.value)}
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
                        className="p-2 text-black block w-full border rounded-xl border-black font-regular bg-white sm:text-sm sm:leading-6"
                        onChange={e => setPassword(e.target.value)}
                      />
                      <p className="mt-2 text-sm text-gray-500">
                        La contraseña debe contener al menos
                      </p>
                      <ul className="flex flex-col gap-1 px-4 mb-2">
                        <li className=" text-xs list-disc">Una Mayúscula</li>
                        <li className=" text-xs list-disc">Un número</li>
                        <li className=" text-xs list-disc">8 Caracteres</li>
                      </ul>
                    </div>
                  </div>

                  <button
                    type="submit"
                    className="p-2 rounded-xl bg-terciary-100 text-white font-bold mt-4 "
                    onClick={() => registerNewUser()}
                  >
                    Registrarme
                  </button>

                  <div className="flex flex-col gap-3 mt-5 mx-auto r justify-end">
                    <Link to={"/login"}>
                      <p className="text-slate-700 text-center text-xs sm:text-sm font-semibold justify-end text-pallete-grey">
                        Ya tengo cuenta
                      </p>
                    </Link>
                  </div>
                </div>
              </form>
            </div>
          </main>
        </div>
      </>
    </MainLayout>
  );
};

export default Register;
