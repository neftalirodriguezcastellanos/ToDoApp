import { useState } from "react";
import useApi from "../../hooks/useApi";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { endpoint_login } from "../../settings/ApiConfig";
import type { ResponseGeneric, LoginResponse } from "../../models/Interfaces";

type LoginFormData = {
  email: string;
  password: string;
};

const useLoginHandler = () => {
  const { loading, error, post } = useApi();
  const { login } = useAuth();
  const navigate = useNavigate();

  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleLogin = async (email: string, password: string) => {
    try {
      const response = await post<
        LoginFormData,
        ResponseGeneric<LoginResponse>
      >(endpoint_login, { email, password });
      console.log("🚀 ~ handleLogin ~ error:", error);
      if (response) {
        if (response.isSuccess) {
          const token = response?.data?.dataAuth.token;
          if (typeof token === "string") {
            login(token);
            navigate("/dashboard");
          } else {
            setErrorMessage("Token inválido al iniciar sesión");
          }
        } else {
          setErrorMessage(response.message || "Error al iniciar sesión");
        }
      } else {
        setErrorMessage(error || "Error al iniciar sesión");
      }
    } catch (err) {
      const message = err instanceof Error ? err.message : "Error desconocido";
      setErrorMessage(message);
      return;
    }
  };

  return { loading, error, errorMessage, handleLogin };
};

export { useLoginHandler };
export type { LoginFormData };
