import React from "react";
import { Link } from "react-router-dom";
import GoogleIcon from "@mui/icons-material/Google";
import FacebookIcon from "@mui/icons-material/Facebook";
import axios from "axios";
import { useState, useEffect } from "react";
import { useContext } from "react";
import { UserContext } from "../store/userContext";
import imageFon from "../images/imageFon.png";
import imageFonTwo from "../images/img_myreports.png";
import { useNavigate } from "react-router-dom";
import { MainLayout } from "../layout/MainLayout";

const SignIn = () => {
  const userCtx = useContext(UserContext);
  const navigate = useNavigate();
  const [user, setUser] = useState("");
  const [password, setPassword] = useState("");
  const [redirect_to, setRedirect_to] = useState();

  const loginAccount = () => {
    const userToLog = {
      user: user,
      password: password,
      redirect_to: redirect_to,
    };

    axios
      .post("https://s10nc.somee.com/api/Auth/login", userToLog)
      .then(res => {
        localStorage.setItem("userSession", JSON.stringify(userToLog));
        setTimeout(() => {
          userCtx.updateUserName(res.data.name);
          userCtx.updateUserEmail(res.data.email);
          userCtx.updateUserProfileImage(res.data.picture_Url);
        }, 400);
        setTimeout(() => {
          navigate("/muro");
        });
      })
      .catch(err => {
        console.log(err);
      });
  };

  return (
    <MainLayout>
      <>
        <div className="flex mt-12 gap-12 justify-center">
          <main className="flex justify-end w-[30em] items-center  gap-10 sm:gap-0 my-6 mx-2 sm:mx-5 md:mx-10">
            <div className="bg-white basis-1/2 w-[50vh] flex flex-1 flex-col sm:gap-0 justify-center p-6 py-8 sm:py-6 lg:px-8 rounded-lg shadow-md">
              <h2 className="text-center text-2xl font-bold leading-9 tracking-tight text-black">
                Iniciar Sesión
              </h2>

              <div className=" flex flex-col gap-6 sm:gap-4 mt-9 sm:mx-auto sm:w-full sm:max-w-sm">
                <div>
                  <div className="mt-2">
                    <input
                      id="Email"
                      name="user"
                      placeholder="Email"
                      type="text"
                      required
                      className="p-2 bg-white rounded-md block w-full border border-black font-regular text-black
                                            sm:text-sm sm:leading-6"
                      onChange={e => setUser(e.target.value)}
                    />
                  </div>
                </div>

                <div>
                  <div className="mt-2">
                    <input
                      id="Contraseña"
                      name="Contraseña"
                      placeholder="Contraseña"
                      type="password"
                      required
                      className="p-2 bg-white rounded-md block w-full border border-black font-regular text-black
                                            sm:text-sm sm:leading-6"
                      onChange={e => setPassword(e.target.value)}
                    />
                    <div className="flex flex-grow justify-end">
                      <button className="mt-2 text-xs text-slate-700 underline cursor-pointer">
                        Olvide la contraseña
                      </button>
                    </div>
                  </div>
                </div>

                <button
                  className="p-2 bg-terciary-100 hover:bg-terciary-50 transition-colors border-none text-center font-bold text-white rounded-2xl"
                  onClick={() => loginAccount()}
                >
                  Iniciar Sesión
                </button>
                <p className="text-black text-center">O</p>

                <div className="flex gap-2 justify-center items-center ">
                  <FacebookIcon />
                  <button className="p-2 w-full border border-terciary-100 text-black  bg-white rounded-2xl text-center font-regular">
                    Iniciar Sesión con Google
                  </button>
                </div>

                <div className="flex gap-2 justify-center items-center ">
                  <FacebookIcon />
                  <button className="p-2 w-full border border-terciary-100 text-black  bg-white rounded-2xl text-center font-regular">
                    Iniciar Sesión con Meta
                  </button>
                </div>

                <div className="flex flex-col gap-3 mt-5 mx-auto items-center justify-center">
                  <Link to={"/register"}>
                    <p className="text-slate-700 text-center text-xs sm:text-sm font-semibold">
                      Registrarse con Email
                    </p>
                  </Link>
                </div>
              </div>
            </div>
          </main>
        </div>
      </>
    </MainLayout>
  );
};

export default SignIn;
