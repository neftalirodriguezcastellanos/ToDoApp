import { useState } from "react";
import useApi from "../../hooks/useApi";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { endpoint_login, endpoint_register } from "../../settings/ApiConfig";
import type { ResponseGeneric, LoginResponse } from "../../models/Interfaces";

type LoginFormData = {
  email: string;
  password: string;
};

type RegisterFormData = {
  email: string;
  fullName: string;
  password: string;
  roles?: string[];
};

const useLoginHandler = () => {
  const { loading, error, post } = useApi();
  const { login, setFullName, setAlert } = useAuth();
  const navigate = useNavigate();

  const [isRegister, setIsRegister] = useState(false);

  const handleLogin = async (email: string, password: string) => {
    try {
      const response = await post<
        LoginFormData,
        ResponseGeneric<LoginResponse>
      >(endpoint_login, { email, password });
      if (response) {
        if (response.isSuccess) {
          setFullName(response.data?.fullName || null);
          const token = response?.data?.dataAuth.token;
          if (typeof token === "string") {
            login(token);
            navigate("/dashboard");
          } else {
            setAlert({ message: "Token inválido", severity: "error" });
          }
        } else {
          setAlert({ message: response.message, severity: "error" });
        }
      } else {
        setAlert({ message: "Error al iniciar sesión", severity: "error" });
      }
    } catch (err) {
      const message = err instanceof Error ? err.message : "Error desconocido";
      setAlert({ message: message, severity: "error" });
      return;
    }
  };

  const handleRegister = async (data: {
    fullName: string;
    email: string;
    password: string;
  }) => {
    const payload: RegisterFormData = { ...data, roles: ["User"] };

    const response = await post<
      RegisterFormData,
      ResponseGeneric<LoginResponse>
    >(endpoint_register, payload);

    if (response) {
      if (response.isSuccess) {
        setIsRegister(false);
        setAlert({
          message: "El usuario se creó correctamente, por favor inicie sesión",
          severity: "success",
        });
      } else {
        setAlert({ message: response.message, severity: "error" });
      }
    } else {
      setAlert({ message: "Error al registrarse", severity: "error" });
    }
  };

  return {
    loading,
    error,
    handleLogin,
    handleRegister,
    isRegister,
    setIsRegister,
  };
};

export { useLoginHandler };
export type { LoginFormData };
