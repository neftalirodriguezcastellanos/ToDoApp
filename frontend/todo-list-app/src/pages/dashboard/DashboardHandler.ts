import { useState } from "react";
import type { ResponseGeneric, Task } from "../../models/Interfaces";
import useApi from "../../hooks/useApi";
import { endpoint_tasks } from "../../settings/ApiConfig";
import type { TaskFormData } from "../../models/Interfaces";
import { useAuth } from "../../context/AuthContext";

const useDashboardHandler = () => {
  const { setAlert } = useAuth();
  const { get, del, put, error, loading } = useApi();
  const [tasksList, setTasksList] = useState<Task[]>([]);
  const [task, setTask] = useState<Task | null>(null);

  const [openModal, setOpenModal] = useState<boolean>(false);
  const [openConfirmDialog, setOpenConfirmDialog] = useState<boolean>(false);

  const getTasks = async () => {
    try {
      const response = await get<ResponseGeneric<Task[]>>("/tasks");
      if (response && response.isSuccess && response.data) {
        setTasksList(response.data);
      } else {
        setTasksList([]);
      }
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
    } catch (error) {
      setTasksList([]);
    }
  };

  const handleUpdateTask = (task: Task) => {
    setTask(task);
    setOpenModal(true);
  };

  const handleCreateTask = () => {
    setTask(null);
    setOpenModal(true);
  };

  const handleConfirmDelete = (task: Task) => {
    setTask(task);
    setOpenConfirmDialog(true);
  };

  const handleDeleteTask = async () => {
    try {
      const response = await del<ResponseGeneric<boolean>>(
        `${endpoint_tasks}/${task?.id}`
      );
      if (response && response.isSuccess) {
        await getTasks();
      } else {
        setAlert({
          message: "Error al eliminar la tarea",
          severity: "error",
        });
      }
    } catch (error) {
      setAlert({
        message:
          "Error al eliminar la tarea: " +
          (error instanceof Error ? error.message : "Error desconocido"),
        severity: "error",
      });
    } finally {
      setOpenConfirmDialog(false);
      setTask(null);
    }
  };

  const handleChangeCheck = async (
    event: React.ChangeEvent<HTMLInputElement>,
    id: string
  ) => {
    const payload: TaskFormData = {
      id: id,
      isCompleted: event.target.checked,
    };

    const response = await put<TaskFormData, ResponseGeneric<boolean>>(
      endpoint_tasks,
      payload
    );

    if (response) {
      if (response.isSuccess) {
        getTasks();
      } else {
        setAlert({
          message:
            response.message || "Error al actualizar el estado de la tarea",
          severity: "error",
        });
      }
    } else {
      setAlert({
        message: "Error al actualizar el estado de la tarea",
        severity: "error",
      });
    }
  };

  return {
    tasksList,
    getTasks,
    error,
    loading,
    setOpenModal,
    openModal,
    task,
    handleUpdateTask,
    handleCreateTask,
    handleConfirmDelete,
    openConfirmDialog,
    setOpenConfirmDialog,
    handleDeleteTask,
    handleChangeCheck,
  };
};

export default useDashboardHandler;
