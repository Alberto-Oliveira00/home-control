import { create } from "zustand";
import api from "../services/api";
import { type TotalsReport } from "../types";

interface TotalsState {
    report: TotalsReport | null;
    isLoading: boolean;
    error: string | null;
    fetchTotals: () => Promise<void>;
}

export const useTotalsStore = create<TotalsState>((set) => ({
  report: null,
  isLoading: false,
  error: null,

  fetchTotals: async () => {
    set({ isLoading: true, error: null });
    try {
      const response = await api.get('/Totals/persons');
      set({ report: response.data, isLoading: false });
    } catch (error) {
      set({ error: 'Erro ao carregar totais', isLoading: false });
    }
  }
}));