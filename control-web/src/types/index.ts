export type TransactionType = 1 | 2;

export const TransactionType = {
  Expense: 1 as const,
  Income: 2 as const,
};

export type CategoryType = 1 | 2 | 3;

export const CategoryType = {
  Expense: 1 as const,
  Income: 2 as const,
  Both: 3 as const,
};

export interface User {
  userId: number;
  name: string;
  age: number;
}

export interface Category {
  categoryId: number;
  description: string;
  type: CategoryType;
}

export interface Transaction {
  transactionId: number;
  description: string;
  value: number;
  type: TransactionType;
  categoryId: number;
  userId: number;
}

// Interfaces para os relatórios (Dashboard)
export interface PersonTotal {
  userId: number;
  name: string;
  totalIncomes: number;
  totalExpenses: number;
  balance: number;
}

export interface TotalsReport {
  items: PersonTotal[];
  generalTotalIncomes: number;
  generalTotalExpenses: number;
  generalBalance: number;
}