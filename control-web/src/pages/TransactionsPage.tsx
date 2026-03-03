import { useEffect } from 'react';
import { useTransactionStore } from '../stores/transactionStore';
import { useUserStore } from '../stores/userStore';
import { useCategoryStore } from '../stores/categoryStore';
import { TransactionType, CategoryType } from '../types';
import { Table, Button, Form, Input, InputNumber, Select, Typography, Card, message, Tag } from 'antd';

const { Title } = Typography;
const { Option } = Select;

export default function TransactionsPage() {
  const { transactions, isLoading, error, fetchTransactions, addTransaction } = useTransactionStore();
  const { users, fetchUsers } = useUserStore();
  const { categories, fetchCategories } = useCategoryStore();
  const [form] = Form.useForm();

  useEffect(() => {
    if (error) message.error(error);
  }, [error]);

  useEffect(() => {
    fetchTransactions();
    fetchUsers();
    fetchCategories();
  }, [fetchTransactions, fetchUsers, fetchCategories]);

  const onFinish = async (values: any) => {
    const success = await addTransaction(values.description, values.value, values.type, values.categoryId, values.userId);
    if (success) {
      form.resetFields(['description', 'value']);
      message.success('Transação lançada!');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'transactionId', key: 'transactionId' },
    { title: 'Pessoa', dataIndex: 'userId', render: (id: number) => users.find(u => u.userId === id)?.name },
    { title: 'Descrição', dataIndex: 'description', key: 'description' },
    { title: 'Categoria', dataIndex: 'categoryId', render: (id: number) => categories.find(c => c.categoryId === id)?.description },
    { 
      title: 'Tipo', dataIndex: 'type', 
      render: (type: number) => type === TransactionType.Expense ? <Tag color="red">Despesa</Tag> : <Tag color="green">Receita</Tag> 
    },
    { 
      title: 'Valor', dataIndex: 'value', 
      render: (val: number) => `R$ ${val.toFixed(2)}` 
    },
  ];

  return (
    <div>
      <Title level={2}>Gerenciar Transações</Title>

      <Card style={{ marginBottom: 24 }}>
        <Form form={form} layout="vertical" onFinish={onFinish} initialValues={{ type: TransactionType.Expense }}>
          <div style={{ display: 'flex', gap: '16px', flexWrap: 'wrap' }}>
            <Form.Item name="description" label="Descrição" rules={[{ required: true }]} style={{ flex: 2, minWidth: '200px' }}>
              <Input placeholder="Ex: Conta de Luz" />
            </Form.Item>
            
            <Form.Item name="value" label="Valor" rules={[{ required: true }]} style={{ flex: 1, minWidth: '120px' }}>
              <InputNumber prefix="R$" style={{ width: '100%' }} min={0.01} step={0.01} />
            </Form.Item>

            <Form.Item name="type" label="Tipo" rules={[{ required: true }]} style={{ flex: 1, minWidth: '120px' }}>
              <Select onChange={() => form.setFieldsValue({ categoryId: undefined })}>
                <Option value={TransactionType.Expense}>Despesa</Option>
                <Option value={TransactionType.Income}>Receita</Option>
              </Select>
            </Form.Item>

            <Form.Item noStyle dependencies={['type']}>
              {({ getFieldValue }) => {
                const selectedType = getFieldValue('type');
                const validCategories = categories.filter(c => c.type === selectedType || c.type === CategoryType.Both);
                return (
                  <Form.Item name="categoryId" label="Categoria" rules={[{ required: true }]} style={{ flex: 1.5, minWidth: '150px' }}>
                    <Select placeholder="Selecione">
                      {validCategories.map(c => <Option key={c.categoryId} value={c.categoryId}>{c.description}</Option>)}
                    </Select>
                  </Form.Item>
                );
              }}
            </Form.Item>

            <Form.Item name="userId" label="Pessoa" rules={[{ required: true }]} style={{ flex: 1.5, minWidth: '150px' }}>
              <Select placeholder="Selecione">
                {users.map(u => <Option key={u.userId} value={u.userId}>{u.name}</Option>)}
              </Select>
            </Form.Item>
          </div>
          <Button type="primary" htmlType="submit" loading={isLoading}>Lançar Transação</Button>
        </Form>
      </Card>

      <Table dataSource={transactions} columns={columns} rowKey="transactionId" loading={isLoading} />
    </div>
  );
}