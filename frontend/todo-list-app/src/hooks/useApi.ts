// src/hooks/useApi.ts
import { useState, useCallback } from "react";
import axios, { AxiosError } from "axios";
import { type ApiError } from "../models/Interfaces";

const useApi = () => {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const [data, setData] = useState<any | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL || "https://localhost:5001/api",
    headers: { "Content-Type": "application/json" },
  });

  // Interceptor para añadir el Bearer automáticamente
  api.interceptors.request.use((config) => {
    const token = localStorage.getItem("authToken");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  });

  const get = useCallback(async <R>(endpoint: string): Promise<R | null> => {
    setLoading(true);
    setError(null);
    try {
      const response = await api.get<R>(endpoint);
      setData(response.data);
      return response.data;
    } catch (err) {
      const error = err as AxiosError<ApiError>;
      if (error.response?.data?.errors) {
        const messages = Object.values(error.response.data.errors).flat();
        setError(messages.join(" "));
      } else {
        setError("Error al obtener los datos");
      }
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  const post = useCallback(
    async <T, R>(endpoint: string, model: T): Promise<R | null> => {
      setLoading(true);
      setError(null);
      try {
        const response = await api.post<R>(endpoint, model);
        setData(response.data);
        return response.data;
      } catch (err) {
        const error = err as AxiosError<ApiError>;
        if (error.response?.data?.errors) {
          const messages = Object.values(error.response.data.errors).flat();
          setError(messages.join(" "));
        } else {
          setError("Error al crear el registro");
        }
        return null;
      } finally {
        setLoading(false);
      }
    },
    []
  );

  const put = useCallback(
    async <T, R>(endpoint: string, model: T): Promise<R | null> => {
      setLoading(true);
      setError(null);
      try {
        const response = await api.put<R>(endpoint, model);
        setData(response.data);
        return response.data;
      } catch (err) {
        const error = err as AxiosError<ApiError>;
        if (error.response?.data?.errors) {
          const messages = Object.values(error.response.data.errors).flat();
          setError(messages.join(" "));
        } else {
          setError("Error al actualizar el registro");
        }
        return null;
      } finally {
        setLoading(false);
      }
    },
    []
  );

  const del = useCallback(async <R>(endpoint: string): Promise<R | null> => {
    setLoading(true);
    setError(null);
    try {
      const response = await api.delete(endpoint);
      setData(response.data);
      return response.data;
    } catch (err) {
      const error = err as AxiosError<ApiError>;
      if (error.response?.data?.errors) {
        const messages = Object.values(error.response.data.errors).flat();
        setError(messages.join(" "));
      } else {
        setError("Error al eliminar el registro");
      }
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  const reset = useCallback(() => {
    setData(null);
    setError(null);
  }, []);

  return { data, loading, error, get, post, put, del, reset };
};

export default useApi;
