import { useState } from "react";
import type { ResponseGeneric, Task } from "../../models/Interfaces";
import useApi from "../../hooks/useApi";
import { endpoint_tasks } from "../../settings/ApiConfig";

const useDashboardHandler = () => {
  const { get, del, error, loading } = useApi();
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
    } catch (error) {
      console.error("Error fetching tasks:", error);
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
        console.error("Failed to delete task");
      }
    } catch (error) {
      console.error("Error deleting task:", error);
    } finally {
      setOpenConfirmDialog(false);
      setTask(null);
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
  };
};

export default useDashboardHandler;
