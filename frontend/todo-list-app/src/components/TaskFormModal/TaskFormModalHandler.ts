import useApi from "../../hooks/useApi";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import type { ResponseGeneric, Task } from "../../models/Interfaces";
import { endpoint_tasks } from "../../settings/ApiConfig";

type TaskFormData = {
  id?: string;
  title: string;
  description?: string;
  dueDate?: string;
  color?: string;
};

const modalStyle = {
  // eslint-disable-next-line @typescript-eslint/prefer-as-const
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: { xs: "90%", sm: 400 },
  bgcolor: "background.paper",
  borderRadius: 2,
  boxShadow: 24,
  p: 4,
};

const colors = [
  { name: "Rojo", value: "#FFADAD" },
  { name: "Amarillo", value: "#FFD6A5" },
  { name: "Verde", value: "#CAFFBF" },
  { name: "Azul", value: "#A0C4FF" },
  { name: "Morado", value: "#D7B2FF" },
  { name: "Gris", value: "#D3D3D3" },
];

interface TaskFormModalProps {
  onTaskCreated: () => void;
  onClose: () => void;
  task?: Task;
}

const useTaskFormModalHandler = ({
  onTaskCreated,
  onClose,
  task,
}: TaskFormModalProps) => {
  const { post, put, error, loading } = useApi();
  const {
    register,
    handleSubmit,
    reset,
    formState: { isSubmitting, errors },
  } = useForm<TaskFormData>({
    defaultValues: {
      id: task?.id || undefined,
      title: task?.title || "",
      description: task?.description || "",
      dueDate: task?.dueDate ? task.dueDate.toString().split("T")[0] : "",
      color: task?.color || "#D3D3D3",
    },
  });

  useEffect(() => {
    if (task) {
      reset({
        id: task.id,
        title: task.title,
        description: task.description || "",
        dueDate: task.dueDate ? task.dueDate.toString().split("T")[0] : "",
        color: task.color || "#D3D3D3",
      });
    } else {
      reset({
        title: "",
        description: "",
        dueDate: "",
        color: "#D3D3D3",
      });
    }
  }, [task, reset]);

  const [submitError, setSubmitError] = useState<string | null>(null);

  const onSubmit = async (data: TaskFormData) => {
    setSubmitError(null);

    const payload = {
      title: data.title.trim(),
      ...(data.id && { id: data.id }),
      ...(data.description && { description: data.description.trim() }),
      ...(data.dueDate && { dueDate: data.dueDate }),
      ...(data.color && { color: data.color }),
    };

    let response = null;

    if (task) {
      response = await put<TaskFormData, ResponseGeneric<boolean>>(
        endpoint_tasks,
        payload
      );
    } else {
      response = await post<TaskFormData, ResponseGeneric<boolean>>(
        endpoint_tasks,
        payload
      );
    }

    if (response) {
      if (response.isSuccess) {
        reset();
        onTaskCreated();
        onClose();
      } else {
        setSubmitError(
          response.message ||
            `No se pudo ${
              task ? "crear" : "actualizar"
            } la tarea. Revisa los datos e inténtalo de nuevo.`
        );
      }
    } else {
      setSubmitError(error || "Error al iniciar sesión");
    }
  };

  return {
    submitError,
    onSubmit,
    isSubmitting,
    errors,
    loading,
    register,
    handleSubmit,
  };
};

export { useTaskFormModalHandler, modalStyle, colors };
export type { TaskFormData };
